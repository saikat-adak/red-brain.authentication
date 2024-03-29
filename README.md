# RedBrain user management and authentication service

It is an API, built with ASP.NET Core 3.1, provides user registration, login with JWT authentication, and user management. It uses the SQLite database in development and a SQL Server in the production environment. It uses EF Core for database connectivity and data migrations.


## Run or debug locally
Download or clone the project and start the kestrel server by running 'dotnet run' from the command line in the project root folder (where the RedBrain.Authentication.csproj file is located), you should see the message 'Now listening on: http://localhost:5000'. Run the following curl commands to check different features:


## Register a new user
To register a new user with the API, just run the following curl command:

**Request**

    curl --location --request POST 'http://localhost:5000/v1/users' \
    --header 'Content-Type: application/json' \
    --data-raw '{
        "firstName": "Saikat",
        "lastName": "Adak",
        "username": "saikat.adak",
        "email": "saikat@xyz.com",
        "mobile": "9999998888",
        "password": "test123",
        "tenant": "my-first-app"
    }'

**Response**

Status: 200 OK


## Authenticate a user by user id and password
To authenticate a registered user and get a JWT token run the following command:

**Request**

    curl --location --request POST 'http://localhost:5000/v1/sessions' \
    --header 'Content-Type: application/json' \
    --data-raw '{
        "username": "saikat.adak",
        "password": "test123",
        "tenant": "my-first-app"
    }'

**Response**

Status: 200 OK  
Body:

    {
        "id": 19,
        "username": "saikat.adak",
        "firstName": "Saikat",
        "lastName": "Adak",
        "email": "saikat@xyz.com",
        "mobile": "9999998888",
        "tenant": "my-first-app",
        "tokenType": "Bearer",
        "token": "xxxx.xxxxx.xxxxx"
    }


## Validate a token
To validate a token run the following command:

**Request**

    curl --location --request GET 'http://localhost:5000/v1/sessions' \
    --header 'Authorization: Bearer xxxx.xxxxx.xxxxx' \
    --data-raw ''

**Response**

Status: 200 OK  
Body:

    {
        "id": 19,
        "username": "saikat.adak",
        "firstName": "Saikat",
        "lastName": "Adak",
        "email": "saikat@xyz.com",
        "mobile": "9999998888",
        "tenant": "my-first-app"
    }


## Get a user by id
To get a user by id run the following command:

**Request**

    curl --location --request GET 'http://localhost:5000/v1/users/19' \
    --header 'Authorization: Bearer xxxx.xxxxx.xxxxx' \
    --data-raw ''

**Response**

Status: 200 OK  
Body:

    {
        "id": 19,
        "username": "saikat.adak",
        "firstName": "Saikat",
        "lastName": "Adak",
        "email": "saikat@xyz.com",
        "mobile": "9999998888",
        "tenant": "my-first-app"
    }


## Update a registered user
Run the following command:

**Request**

    curl --location --request PUT 'http://localhost:5000/v1/users/19' \
    --header 'Authorization: Bearer xxxx.xxxxx.xxxxx' \
    --header 'Content-Type: application/json' \
    --data-raw '{
        "firstName": "John",
        "lastName": "Doe"
    }'

**Response**

Status: 200 OK


## Make an authenticated request to retrieve all the users
To make an authenticated request using the JWT token from the previous step, run this command:

**Request**

    curl --location --request GET 'http://localhost:5000/v1/users' \
    --header 'Authorization: Bearer xxxx.xxxxx.xxxxx' \
    --data-raw ''

**Response**

Status: 200 OK  
Body:

    [
        {
            "id": 19,
            "username": "saikat.adak",
            "firstName": "Saikat",
            "lastName": "Adak",
            "email": "saikat@xyz.com",
            "tenant": "my-first-app",
            "mobile": "9999998888"
        }
    ]


## Postman test runner
All these features can be tested by running a collection in Postman (PFA 'postman-collection.json' in the root directory).