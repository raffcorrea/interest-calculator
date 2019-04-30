FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["src/InterestCalculator.WebApi/InterestCalculator.WebApi.csproj", "src/InterestCalculator.WebApi/"]
RUN dotnet restore "src/InterestCalculator.WebApi/InterestCalculator.WebApi.csproj"
COPY . .
WORKDIR "/src/src/InterestCalculator.WebApi"
RUN dotnet build "InterestCalculator.WebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "InterestCalculator.WebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "InterestCalculator.WebApi.dll"]