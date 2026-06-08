FROM node:22-alpine AS build-front
WORKDIR /app
COPY frontend/contacts-frontend/package*.json ./frontend/
WORKDIR /app/frontend
RUN npm ci
COPY frontend/contacts-frontend/ ./
RUN npm run build

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-back
WORKDIR /src
COPY backend/ ./backend/
RUN dotnet publish "backend/CRUD.App/CRUD.App.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-back /app/publish .
COPY --from=build-front /app/frontend/dist ./wwwroot
EXPOSE 8080
ENTRYPOINT ["dotnet", "CRUD.App.dll"]
