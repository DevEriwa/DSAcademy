-- Run this SQL manually on your DSAcademy_App database if the EF migration
-- fails to apply automatically, or if you prefer to apply the schema change
-- directly without running 'dotnet ef migrations add'.

ALTER TABLE CompanySettings
    ADD PrimaryColor   NVARCHAR(10)  NULL DEFAULT '#0A192F',
        SecondaryColor NVARCHAR(10)  NULL DEFAULT '#FFB300',
        SidebarColor   NVARCHAR(10)  NULL DEFAULT '#112B50',
        FontFamily     NVARCHAR(60)  NULL DEFAULT 'Outfit',
        DarkMode       BIT           NOT NULL DEFAULT 0;
