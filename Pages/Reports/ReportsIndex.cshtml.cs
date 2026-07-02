using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SupermarketManagementAditya.Data;
using SupermarketManagementAditya.Models;

namespace SupermarketManagementAditya.Pages.Reports
{
    public class ReportsIndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ReportsIndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Product> LowStockItems { get; set; } = new();
        public List<CategoryReport> ProductsByCategory { get; set; } = new();
        public List<SupplierStockReport> SupplierStockLists { get; set; } = new();
        public List<SalesByProductReport> SalesByProduct { get; set; } = new();

        public void OnGet()
        {
            LoadReports();
        }

        public IActionResult OnGetDownloadPdf()
        {
            QuestPDF.Settings.License = LicenseType.Community;

            LoadReports();

            var pdfBytes = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);

                    page.Header()
                        .Text("Supermarket Management Report")
                        .FontSize(20)
                        .Bold()
                        .AlignCenter();

                    page.Content().Column(column =>
                    {
                        column.Spacing(15);

                        column.Item().Text("Low Stock Items").FontSize(14).Bold();
                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).Text("Product").Bold();
                                header.Cell().Element(CellStyle).Text("Supplier").Bold();
                                header.Cell().Element(CellStyle).Text("Quantity").Bold();
                                header.Cell().Element(CellStyle).Text("Status").Bold();
                            });

                            foreach (var item in LowStockItems)
                            {
                                table.Cell().Element(CellStyle).Text(item.ProductName ?? "");
                                table.Cell().Element(CellStyle).Text(item.Supplier ?? "");
                                table.Cell().Element(CellStyle).Text(item.Quantity.ToString());
                                table.Cell().Element(CellStyle).Text(item.StockStatus ?? "");
                            }
                        });

                        column.Item().Text("Sales By Product").FontSize(14).Bold();
                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).Text("Product").Bold();
                                header.Cell().Element(CellStyle).Text("Quantity Sold").Bold();
                                header.Cell().Element(CellStyle).Text("Revenue").Bold();
                            });

                            foreach (var sale in SalesByProduct)
                            {
                                table.Cell().Element(CellStyle).Text(sale.ProductName ?? "");
                                table.Cell().Element(CellStyle).Text(sale.TotalQuantitySold.ToString());
                                table.Cell().Element(CellStyle).Text("£" + sale.TotalRevenue.ToString("0.00"));
                            }
                        });

                        column.Item().Text("Products By Category").FontSize(14).Bold();
                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).Text("Category").Bold();
                                header.Cell().Element(CellStyle).Text("Total Products").Bold();
                                header.Cell().Element(CellStyle).Text("Total Stock").Bold();
                            });

                            foreach (var item in ProductsByCategory)
                            {
                                table.Cell().Element(CellStyle).Text(item.Category ?? "");
                                table.Cell().Element(CellStyle).Text(item.TotalProducts.ToString());
                                table.Cell().Element(CellStyle).Text(item.TotalStock.ToString());
                            }
                        });

                        column.Item().Text("Supplier Stock List").FontSize(14).Bold();
                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).Text("Supplier").Bold();
                                header.Cell().Element(CellStyle).Text("Total Products").Bold();
                                header.Cell().Element(CellStyle).Text("Total Stock").Bold();
                            });

                            foreach (var item in SupplierStockLists)
                            {
                                table.Cell().Element(CellStyle).Text(item.Supplier ?? "");
                                table.Cell().Element(CellStyle).Text(item.TotalProducts.ToString());
                                table.Cell().Element(CellStyle).Text(item.TotalStock.ToString());
                            }
                        });
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text("Generated on " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                });
            }).GeneratePdf();

            return File(pdfBytes, "application/pdf", "Supermarket_Report.pdf");
        }

        private void LoadReports()
        {
            LowStockItems = _context.Products
                .Where(p => p.Quantity <= 5)
                .OrderBy(p => p.Quantity)
                .ToList();

            ProductsByCategory = _context.Products
                .GroupBy(p => p.Category)
                .Select(g => new CategoryReport
                {
                    Category = g.Key,
                    TotalProducts = g.Count(),
                    TotalStock = g.Sum(p => p.Quantity)
                })
                .ToList();

            SupplierStockLists = _context.Products
                .GroupBy(p => p.Supplier)
                .Select(g => new SupplierStockReport
                {
                    Supplier = g.Key,
                    TotalProducts = g.Count(),
                    TotalStock = g.Sum(p => p.Quantity)
                })
                .ToList();

            SalesByProduct = _context.Sales
                .GroupBy(s => s.ProductName)
                .Select(g => new SalesByProductReport
                {
                    ProductName = g.Key,
                    TotalQuantitySold = g.Sum(s => s.QuantitySold),
                    TotalRevenue = g.Sum(s => s.TotalAmount)
                })
                .ToList();
        }

        private static IContainer CellStyle(IContainer container)
        {
            return container
                .Border(1)
                .BorderColor(Colors.Grey.Lighten2)
                .Padding(5);
        }
    }

    public class CategoryReport
    {
        public string Category { get; set; } = "";
        public int TotalProducts { get; set; }
        public int TotalStock { get; set; }
    }

    public class SupplierStockReport
    {
        public string Supplier { get; set; } = "";
        public int TotalProducts { get; set; }
        public int TotalStock { get; set; }
    }

    public class SalesByProductReport
    {
        public string ProductName { get; set; } = "";
        public int TotalQuantitySold { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}