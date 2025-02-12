# DomainSpace

**DomainSpace** is a content sharing app where users can share and view content, but only with others who have registered with the same email domain. The app has different user roles, each with varying levels of access and permissions.

## Build and Deploy Status

[![Deploy .NET Api](https://github.com/z0lt4np4l1nk4s/DomainSpace/actions/workflows/dotnet.yml/badge.svg)](https://github.com/z0lt4np4l1nk4s/DomainSpace/actions/workflows/dotnet.yml)
[![Deploy React App](https://github.com/z0lt4np4l1nk4s/DomainSpace/actions/workflows/react.yml/badge.svg)](https://github.com/z0lt4np4l1nk4s/DomainSpace/actions/workflows/react.yml)

## Features

- **Content Sharing**: Users can publish content and share it with others in the same email domain.
- **Role-Based Access**:
  - **Normal User**: Can publish content and view content shared with users in the same domain.
  - **Moderator**: Can remove content and add subjects for content.
  - **Admin**: Can manage users, promote them to moderators, and delete user accounts.
- **Domain-Specific Access**: Users can only see content shared with others from their email domain.
- **Subjects**: Content must belong to a subject for better organization.

## Tech Stack

- **Frontend**: React.js
- **Backend**: ASP.NET Core 6 Web API
- **Database**: PostgreSQL

## Try it Out

You can test the app at: [domainspace.zoltan-palinkas.from.hr](http://domainspace.zoltan-palinkas.from.hr)
