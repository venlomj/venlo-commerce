# E-commerce API Endpoints

## Orders Endpoints

### `POST /api/orders/{id:guid}`
Place a new order.

#### Request:
- **URL Parameter**: `id` (Guid) - The unique identifier of the product.
- **Body**: `OrderRequest` - Contains order details (e.g., quantity).

#### Response:
- Status: `200 OK`
- Body: Order result (success or failure)

---

### `GET /api/orders`
Retrieve all orders.

#### Response:
- Status: `200 OK`
- Body: List of all orders

---

### `GET /api/orders/{id:guid}`
Retrieve a specific order by ID.

#### Request:
- **URL Parameter**: `id` (Guid) - The unique identifier of the order.

#### Response:
- Status: `200 OK`
- Body: Order details (Invoice and other details)

---

## Products Endpoints

### `GET /api/products`
Retrieve a list of all products.

#### Response:
- Status: `200 OK`
- Body: List of products

---

### `GET /api/products/paged`
Retrieve a paginated list of products.

#### Request:
- **Query Parameters**: `page` (int) - The page number. (Default: 1)
  `pageSize` (int) - The number of products per page. (Default: 10)

#### Response:
- Status: `200 OK`
- Body: Paginated list of products

---

### `POST /api/products`
Add a new product.

#### Request:
- **Body**: `ProductRequest` - Contains product details (e.g., name, price, description).

#### Response:
- Status: `201 Created`
- Body: The created product

---

### `GET /api/products/{id:guid}`
Retrieve a specific product by ID.

#### Request:
- **URL Parameter**: `id` (Guid) - The unique identifier of the product.

#### Response:
- Status: `200 OK`
- Body: Product details

---

### `GET /api/products/sku`
Retrieve products by a list of SKU codes.

#### Request:
- **Query Parameter**: `skuCodes` (List of strings) - A list of SKU codes to search for.

#### Response:
- Status: `200 OK`
- Body: List of products matching the SKU codes

---

### `PUT /api/products/{id:guid}`
Update a specific product by ID.

#### Request:
- **URL Parameter**: `id` (Guid) - The unique identifier of the product.
- **Body**: `ProductRequest` - Contains updated product details.

#### Response:
- Status: `200 OK`
- Body: The updated product

---

### `DELETE /api/products/{id:guid}`
Delete a specific product by ID.

#### Request:
- **URL Parameter**: `id` (Guid) - The unique identifier of the product.

#### Response:
- Status: `200 OK`
- Body: Confirmation message (Success/Failure)

---

## Pictures Endpoints

### `POST /api/pictures/{productId:guid}`
Upload an image for a specific product.

#### Request:
- **URL Parameter**: `productId` (Guid) - The unique identifier of the product.
- **Body**: `PictureRequest` - Contains image data and other picture-related information.

#### Response:
- Status: `200 OK`
- Body: Result of image upload (success/failure)

---

## Stock Endpoints

### `GET /api/stock`
Retrieve the stock information for all products.

#### Response:
- Status: `200 OK`
- Body: List of stock items (Quantity, Product, etc.)

---

### `GET /api/stock/{productId:guid}`
Retrieve stock information for a specific product.

#### Request:
- **URL Parameter**: `productId` (Guid) - The unique identifier of the product.

#### Response:
- Status: `200 OK`
- Body: Stock details for the product

---

### `POST /api/stock/{productId:guid}`
Add stock for a specific product.

#### Request:
- **URL Parameter**: `productId` (Guid) - The unique identifier of the product.
- **Body**: `StockItemRequest` - Contains stock details (e.g., quantity).

#### Response:
- Status: `200 OK`
- Body: Confirmation message (Success/Failure)

---

### `PUT /api/stock/{productId:guid}`
Update stock for a specific product.

#### Request:
- **URL Parameter**: `productId` (Guid) - The unique identifier of the product.
- **Body**: `StockItemRequest` - Contains updated stock details.

#### Response:
- Status: `200 OK`
- Body: The updated stock information

---

### `DELETE /api/stock/{productId:guid}`
Delete stock for a specific product.

#### Request:
- **URL Parameter**: `productId` (Guid) - The unique identifier of the product.

#### Response:
- Status: `200 OK`
- Body: Confirmation message (Success/Failure)

---
