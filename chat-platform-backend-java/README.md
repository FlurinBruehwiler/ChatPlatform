[![build](https://github.com/Oh-my-class/oh-my-backend/actions/workflows/codeql-analysis.yml/badge.svg)](https://github.com/Oh-my-class/oh-my-backend/blob/develop/.github/workflows/codeql-analysis.yml)
![GitHub release (latest by date)](https://img.shields.io/github/v/release/Oh-my-class/oh-my-backend)
![License](https://img.shields.io/badge/license-GNU--3.0-orange)

# Oh my backend

The REST-ful API of [Oh my class](https://github.com/Oh-my-class)

## How to start the application

1. Clone repository
2. Navigate to the root of the project
   1. Run ```docker-compose up -d``` to start the database
3. Start OhMyClass.Application

## Send requests

**Baseurl: https://localhost:8080/**

| Name          | URI                             | Parameters                                | Return Value                     |
|:------------- |:--------------------------------|:------------------------------------------|:---------------------------------|
| Login         | POST /api/v1/auth/login         | x-www-form-urlencoded: username, password | JWT[access_token, refresh_token] |
| Register      | POST /api/v1/auth/register      | raw: {username, password, email}          | JWT[access_token, refresh_token] |
| Refresh token | POST /api/v1/auth/refresh-token | Auth-header: JWT refresh token            | JWT[access_token]                |