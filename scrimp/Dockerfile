#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 52167
EXPOSE 44308

FROM microsoft/dotnet:2.1-sdk-nanoserver-1803 AS build
WORKDIR /src
COPY ["scrimp/scrimp.csproj", "scrimp/"]
RUN dotnet restore "scrimp/scrimp.csproj"
COPY . .
WORKDIR "/src/scrimp"
RUN dotnet build "scrimp.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "scrimp.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "scrimp.dll"]