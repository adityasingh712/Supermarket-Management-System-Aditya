# Local Supermarket Management System  

## About Project  

This project is a **Local Supermarket Management System** created using **ASP.NET Core Razor Pages, C#, SQL Server and Entity Framework Core**.  

The purpose of this project is to help small supermarkets manage their daily work digitally instead of using paper records. The system makes it easier to manage products, suppliers, stock levels and sales transactions.  

This project was developed as part of **CST2550 Coursework** at Middlesex University London.  

---

## Technologies Used  

The following technologies were used in this project:  

- C#  
- ASP.NET Core Razor Pages  
- SQL Server LocalDB  
- Entity Framework Core  
- Bootstrap  
- Visual Studio 2022  
- GitHub  

---

## Features of System  

The project includes the following features:  

- User Registration  
- User Login  
- Add New Products  
- Edit Product Details  
- Delete Products  
- Search Product by Product Name  
- Search Product by Barcode  
- Product Image Upload  
- Supplier Management  
- Sales Recording System  
- Automatic Revenue Calculation  
- Low Stock Warning System  
- Reorder Soon Warning  
- Expiry Monitoring  
- Expiring Soon Products Page  
- SQL Database Integration  

---

## How Project Works  

### 1. Register and Login  

The user first creates an account using the register page.  

After registration, the user logs in using email and password. Only logged in users can access the dashboard.  

---

### 2. Product Management  

The system allows users to add supermarket products by entering product details such as:  

- Product Name  
- Category  
- Supplier  
- Barcode  
- Price  
- Quantity  
- Manufacturing Date  
- Expiry Date  
- Product Image  

All product information is stored inside SQL Server database.  

---

### 3. Search Function  

The product list page allows searching products by:  

- Product Name  
- Barcode  

This helps users quickly find products available in the system.  

---

### 4. Stock Monitoring  

The system automatically checks stock quantity.  

- If quantity is below 10 → Reorder Soon Warning  
- If quantity is below 5 → Low Stock Warning  

This helps identify products that need restocking.  

---

### 5. Expiry Monitoring  

The system automatically checks expiry dates.  

- Expired products → Expired status shown  
- Products close to expiry → Expiring Soon status shown  

The dashboard also has an **Expiring Soon page** where users can check products close to expiry.  

---

### 6. Sales Recording  

The system allows users to record sales.  

When a product is sold:  

- Product quantity reduces automatically  
- Sale is stored in database  
- Revenue is updated automatically  

---

### 7. Supplier Management  

Products are connected with suppliers.  

Users can:  

- View supplier products  
- Edit supplier details  
- Manage supplier information  

---

## Database Used  

This project uses **SQL Server LocalDB** connected using **Entity Framework Core**.  

Main database tables used are:  

- Users  
- Products  
- Sales  

Database connection settings are stored inside:  

```text
appsettings.json
```

---

## How to Run Project  

Step 1  

Open project in Visual Studio 2022  

Step 2  

Restore NuGet packages  

Step 3  

Check database connection inside:  

```text
appsettings.json
```

Step 4  

Run database migration command:  

```bash
Update-Database
```

Step 5  

Run project using:  

```text
Ctrl + F5
```

Step 6  

Register account or use demo login details below  

---

## Demo Login Details  

To test the website, use the following login credentials:  

Email: **aditya712@gmail.com**  

Password: **aditya@712**  

After login, users can access the dashboard and test all supermarket features.  

---

## Author  

**Aditya Singh**  

CST2550 Coursework  

Middlesex University London  
