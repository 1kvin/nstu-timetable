#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["nstu-timetable/nstu-timetable.csproj", "nstu-timetable/"]
RUN dotnet restore "nstu-timetable/nstu-timetable.csproj"
COPY . .
WORKDIR "/src/nstu-timetable"
RUN dotnet build "nstu-timetable.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "nstu-timetable.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "nstu-timetable.dll"]