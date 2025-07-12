# MongoDB Setup Guide for ECommerce Application

## Overview
This guide explains how to set up and use MongoDB with the ECommerce application. The application has been configured to use MongoDB as the database with automatic collection creation.

## Prerequisites
- .NET 8.0 or later
- MongoDB Community Server installed and running locally
- MongoDB Compass (optional but recommended for database management)

## MongoDB Installation

### Windows
1. Download MongoDB Community Server from https://www.mongodb.com/try/download/community
2. Run the installer and follow the installation wizard
3. MongoDB will start automatically as a Windows service on port 27017

### Alternative: Using Docker
```bash
docker run -d -p 27017:27017 --name mongodb mongo:latest
```

## Application Configuration

### Database Connection
The application is configured to connect to MongoDB using the following default settings:
- **Connection String**: `mongodb://localhost:27017`
- **Database Name**: `ECommerceDb` (Production) / `ECommerceDb_Dev` (Development)

### Configuration Files
MongoDB settings are stored in `appsettings.json`:

```json
{
  "MongoDb": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "ECommerceDb"
  }
}
```

## Database Structure

### Collections
The application will automatically create the following collections:
- **products** - Product catalog
- **categories** - Product categories
- **carts** - Shopping carts with embedded cart items

### Entity Models

#### Product
```csharp
{
  "_id": ObjectId,
  "name": string,
  "description": string,
  "price": decimal,
  "stockQuantity": int,
  "categoryId": ObjectId,
  "createdAt": DateTime,
  "updatedAt": DateTime
}
```

#### Category
```csharp
{
  "_id": ObjectId,
  "name": string,
  "description": string,
  "createdAt": DateTime,
  "updatedAt": DateTime
}
```

#### Cart
```csharp
{
  "_id": ObjectId,
  "userId": ObjectId,
  "items": [
    {
      "productId": ObjectId,
      "productName": string,
      "price": decimal,
      "quantity": int,
      "createdAt": DateTime,
      "updatedAt": DateTime
    }
  ],
  "createdAt": DateTime,
  "updatedAt": DateTime
}
```

## API Endpoints

### Products API
- `GET /api/products` - Get all products
- `GET /api/products/{id}` - Get product by ID
- `POST /api/products` - Create new product
- `PUT /api/products/{id}` - Update product
- `DELETE /api/products/{id}` - Delete product

### Categories API
- `GET /api/categories` - Get all categories
- `GET /api/categories/{id}` - Get category by ID
- `POST /api/categories` - Create new category
- `PUT /api/categories/{id}` - Update category
- `DELETE /api/categories/{id}` - Delete category

### Carts API
- `GET /api/carts` - Get all carts
- `GET /api/carts/{id}` - Get cart by ID
- `GET /api/carts/user/{userId}` - Get cart by user ID
- `POST /api/carts` - Create new cart
- `PUT /api/carts/{id}` - Update cart
- `DELETE /api/carts/{id}` - Delete cart

## Running the Application

### Step 1: Start MongoDB
Ensure MongoDB is running on your local machine:
```bash
# Check if MongoDB is running
netstat -an | findstr :27017
```

### Step 2: Build the Solution
```bash
dotnet build ECommerce.sln
```

### Step 3: Run the WebAPI
```bash
dotnet run --project ECommerce.WebAPI
```

### Step 4: Run the WebUI (Optional)
```bash
dotnet run --project ECommerce.WebUI
```

## Testing the API

### Using Swagger
1. Navigate to `https://localhost:7xxx/swagger` (replace xxx with your port)
2. Test the API endpoints directly from the Swagger UI

### Using curl
```bash
# Create a category
curl -X POST "https://localhost:7xxx/api/categories" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Electronics",
    "description": "Electronic devices and gadgets"
  }'

# Create a product
curl -X POST "https://localhost:7xxx/api/products" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Laptop",
    "description": "High-performance laptop",
    "price": 999.99,
    "stockQuantity": 10,
    "categoryId": "your_category_id_here"
  }'
```

## Database Management

### Using MongoDB Compass
1. Install MongoDB Compass
2. Connect to `mongodb://localhost:27017`
3. Browse the `ECommerceDb` database
4. View and manage collections

### Using MongoDB Shell
```bash
# Connect to MongoDB
mongosh

# Switch to the ECommerce database
use ECommerceDb

# View collections
show collections

# Query products
db.products.find()

# Query categories
db.categories.find()
```

## Features

### Automatic Collection Creation
Collections are automatically created when the first document is inserted.

### ObjectId Support
All entities use MongoDB ObjectId for primary keys, automatically generated.

### Embedded Documents
Cart items are embedded within cart documents for better performance.

### Indexing
Consider adding indexes for frequently queried fields:
```javascript
// In MongoDB shell
db.products.createIndex({ "categoryId": 1 })
db.products.createIndex({ "name": "text", "description": "text" })
db.carts.createIndex({ "userId": 1 })
```

## Troubleshooting

### Connection Issues
- Ensure MongoDB is running: `net start MongoDB` (Windows)
- Check firewall settings
- Verify connection string in appsettings.json

### Build Errors
- Ensure all NuGet packages are restored: `dotnet restore`
- Check project references are correct
- Verify MongoDB.Driver version compatibility

### Runtime Errors
- Check MongoDB logs for connection issues
- Verify database permissions
- Ensure proper ObjectId format in API calls

## Best Practices

1. **Connection Pooling**: MongoDB driver automatically handles connection pooling
2. **Error Handling**: Always handle ObjectId parsing errors
3. **Data Validation**: Validate input data before database operations
4. **Indexing**: Create appropriate indexes for query performance
5. **Monitoring**: Monitor database performance and connection metrics

## Next Steps

1. Implement user authentication and authorization
2. Add data validation and business logic
3. Implement caching strategies
4. Add logging and monitoring
5. Consider implementing CQRS pattern for complex queries
6. Add unit and integration tests

For more information, refer to the official MongoDB documentation: https://docs.mongodb.com/