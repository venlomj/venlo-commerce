# VenloCommerce API

## Description

This is a robust e-commerce API built using a clean architecture approach. The API allows interaction with product, order, and image management systems. It supports product listing, order placement, image upload for products, and invoicing functionalities. The application uses **MongoDB** for storing product images as binary files, and **Microsoft SQL Server** for storing other models like products, categories, orders, and stock items.

The API also integrates **AutoMapper** for object-to-object mapping and **FluentValidation** for validation of inputs.

---

## Table of Contents

- [Features](#features)
- [Technologies](#technologies)
- [Setup](#setup)
- [API Endpoints](#api-endpoints)
- [Models](#models)
- [Contributing](#contributing)

---

## Features

- **Order Management**: Place orders, retrieve orders, and view invoices.
- **Product Management**: Add, retrieve, update, and delete products. Retrieve products by SKU code.
- **Image Management**: Upload product images and associate them with products.
- **Category Management**: Manage product categories.

---

## Technologies

- **.NET 9** for backend development
- **MongoDB** for storing product images as binary files
- **Microsoft SQL Server** for other models (products, categories, orders, etc.)
- **AutoMapper** for object mapping
- **FluentValidation** for validating input data
- **MediateR** for handling commands and queries
- **Swagger** for API documentation

---

## Setup

1. **Clone the Repository**:

    ```bash
    git clone <repository-url>
    ```

2. **Install Dependencies**:

    - .NET SDK 9
    - MongoDB for storing images
    - Microsoft SQL Server for relational data

3. **Set up Configuration**:

    Update the `appsettings.json` for the appropriate connection strings (SQL Server and MongoDB).

4. **Run the Application**:

    ```bash
    dotnet run
    ```

---

## API Endpoints

### Orders Endpoints

- **Place an Order**

    `POST /api/orders/{id:guid}`

    Request Body:
    ```json
    {
        "Quantity": 5
    }
    ```

    Response:
    ```json
    {
        "OrderNumber": "ORD12345",
        "TotalPrice": 150.00
    }
    ```

- **Get Orders**

    `GET /api/orders`

    Response:
    ```json
    [
        {
            "OrderNumber": "ORD12345",
            "TotalPrice": 150.00
        }
    ]
    ```

- **Get Order by ID**

    `GET /api/orders/{id:guid}`

    Response:
    ```json
    {
        "OrderNumber": "ORD12345",
        "TotalPrice": 150.00,
        "OrderLineItems": [
            {
                "ProductId": "some-guid",
                "Quantity": 5,
                "Price": 30.00
            }
        ]
    }
    ```

- **Download Invoice**

    `GET /api/orders/{id:guid}/invoice`

    Response:
    - Returns PDF file of the invoice.

---

### Products Endpoints

- **Get All Products**

    `GET /api/products`

    Response:
    ```json
    [
        {
            "Id": "some-guid",
            "Name": "Product 1",
            "Description": "A sample product",
            "Price": 50.00
        }
    ]
    ```

- **Get Product by ID**

    `GET /api/products/{id:guid}`

    Response:
    ```json
    {
        "Id": "some-guid",
        "Name": "Product 1",
        "Description": "A sample product",
        "Price": 50.00
    }
    ```

- **Get Products by SKU**

    `GET /api/products/sku?skuCodes=sku1,sku2`

    Response:
    ```json
    [
        {
            "Id": "some-guid",
            "SkuCode": "sku1",
            "Name": "Product 1",
            "Description": "A sample product"
        }
    ]
    ```

- **Add a Product**

    `POST /api/products`

    Request Body:
    ```json
    {
        "Name": "New Product",
        "Description": "Description of new product",
        "Price": 75.00,
        "CategoryId": "some-category-guid"
    }
    ```

    Response:
    ```json
    {
        "Id": "new-product-guid",
        "Name": "New Product",
        "Price": 75.00
    }
    ```

- **Update Product**

    `PUT /api/products/{id:guid}`

    Request Body:
    ```json
    {
        "Name": "Updated Product",
        "Description": "Updated description",
        "Price": 80.00
    }
    ```

- **Delete Product**

    `DELETE /api/products/{id:guid}`

    Response:
    ```json
    {
        "Message": "Product deleted successfully."
    }
    ```

---

### Pictures Endpoints

- **Upload Image for Product**

    `POST /api/pictures/{productId:guid}`

    Request Body:
    ```json
    {
        "ImageData": "binary-image-data",
        "Name": "Product Image"
    }
    ```

    Response:
    ```json
    {
        "Message": "Image uploaded successfully."
    }
    ```

---

### Models

#### Product

- **Id**: `Guid` (Unique identifier)
- **SkuCode**: `string` (Product SKU code)
- **Name**: `string` (Product name)
- **Description**: `string` (Product description)
- **Price**: `decimal` (Product price)
- **CategoryId**: `Guid` (Foreign key to Category)

#### Category

- **Id**: `Guid` (Unique identifier)
- **Name**: `string` (Category name)
- **Description**: `string` (Category description)
- **Products**: `List<Product>` (Products in this category)

#### Order

- **Id**: `Guid` (Unique identifier)
- **OrderNumber**: `string` (Unique order number)
- **OrderLineItems**: `List<OrderLineItem>` (Items in the order)

#### OrderLineItem

- **Id**: `Guid` (Unique identifier)
- **Price**: `decimal` (Item price)
- **Quantity**: `int` (Quantity ordered)
- **ProductId**: `Guid` (Product reference)
- **OrderId**: `Guid` (Order reference)

#### StockItem

- **Id**: `Guid` (Unique identifier)
- **Quantity**: `int` (Quantity in stock)
- **ProductId**: `Guid` (Product reference)

#### Picture (MongoDB)

- **Id**: `Guid` (Unique identifier)
- **ProductId**: `Guid` (Product reference)
- **ImageData**: `byte[]` (Image data in binary format)
- **Name**: `string` (Image name)

---

## Contributing

Feel free to open issues, fork the repository, and submit pull requests.

---

## License

This project is licensed under the MIT License.
