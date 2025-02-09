CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `AspNetRoles` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(256) CHARACTER SET utf8mb4 NULL,
    `NormalizedName` varchar(256) CHARACTER SET utf8mb4 NULL,
    `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_AspNetRoles` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `AspNetUsers` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `OperatorId` int NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Register` int NOT NULL,
    `Role` longtext CHARACTER SET utf8mb4 NULL,
    `Phone` longtext CHARACTER SET utf8mb4 NULL,
    `UserName` varchar(256) CHARACTER SET utf8mb4 NULL,
    `NormalizedUserName` varchar(256) CHARACTER SET utf8mb4 NULL,
    `Email` varchar(256) CHARACTER SET utf8mb4 NULL,
    `NormalizedEmail` varchar(256) CHARACTER SET utf8mb4 NULL,
    `EmailConfirmed` tinyint(1) NOT NULL,
    `PasswordHash` longtext CHARACTER SET utf8mb4 NULL,
    `SecurityStamp` longtext CHARACTER SET utf8mb4 NULL,
    `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 NULL,
    `PhoneNumber` longtext CHARACTER SET utf8mb4 NULL,
    `PhoneNumberConfirmed` tinyint(1) NOT NULL,
    `TwoFactorEnabled` tinyint(1) NOT NULL,
    `LockoutEnd` datetime(6) NULL,
    `LockoutEnabled` tinyint(1) NOT NULL,
    `AccessFailedCount` int NOT NULL,
    CONSTRAINT `PK_AspNetUsers` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Documents` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `DocumentName` longtext CHARACTER SET utf8mb4 NOT NULL,
    `UploadedAt` datetime(6) NOT NULL,
    CONSTRAINT `PK_Documents` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `MaintenanceNumberControls` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `CurrentNumber` int NOT NULL,
    CONSTRAINT `PK_MaintenanceNumberControls` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Sectors` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Location` longtext CHARACTER SET utf8mb4 NOT NULL,
    `RequesterName` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Address` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Number` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Neighborhood` longtext CHARACTER SET utf8mb4 NOT NULL,
    `City` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Cep` longtext CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_Sectors` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Stocks` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `ProductName` longtext CHARACTER SET utf8mb4 NULL,
    `ProductDescription` longtext CHARACTER SET utf8mb4 NULL,
    `ProductQuantity` int NOT NULL,
    `ProductApplication` longtext CHARACTER SET utf8mb4 NULL,
    `ProductReference` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Stocks` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `AspNetRoleClaims` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `RoleId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `ClaimType` longtext CHARACTER SET utf8mb4 NULL,
    `ClaimValue` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_AspNetRoleClaims` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `AspNetUserClaims` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `UserId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `ClaimType` longtext CHARACTER SET utf8mb4 NULL,
    `ClaimValue` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_AspNetUserClaims` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `AspNetUserLogins` (
    `LoginProvider` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `ProviderKey` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `ProviderDisplayName` longtext CHARACTER SET utf8mb4 NULL,
    `UserId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_AspNetUserLogins` PRIMARY KEY (`LoginProvider`, `ProviderKey`),
    CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `AspNetUserRoles` (
    `UserId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `RoleId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_AspNetUserRoles` PRIMARY KEY (`UserId`, `RoleId`),
    CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `AspNetUserTokens` (
    `UserId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `LoginProvider` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Value` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_AspNetUserTokens` PRIMARY KEY (`UserId`, `LoginProvider`, `Name`),
    CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Devices` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Type` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Identifier` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Model` longtext CHARACTER SET utf8mb4 NOT NULL,
    `SerialNumber` longtext CHARACTER SET utf8mb4 NOT NULL,
    `DeviceDescription` varchar(300) CHARACTER SET utf8mb4 NOT NULL,
    `SectorId` int NOT NULL,
    CONSTRAINT `PK_Devices` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Devices_Sectors_SectorId` FOREIGN KEY (`SectorId`) REFERENCES `Sectors` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `StockManagements` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `MovementDate` datetime(6) NOT NULL,
    `MovementType` int NOT NULL,
    `Quantity` int NOT NULL,
    `StockId` int NOT NULL,
    CONSTRAINT `PK_StockManagements` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_StockManagements_Stocks_StockId` FOREIGN KEY (`StockId`) REFERENCES `Stocks` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Maintenances` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `SimpleDesc` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Description` longtext CHARACTER SET utf8mb4 NOT NULL,
    `OpenDate` datetime(6) NOT NULL,
    `CloseDate` datetime(6) NOT NULL,
    `Status` int NOT NULL,
    `ApplicationUserId` varchar(255) CHARACTER SET utf8mb4 NULL,
    `DeviceId` int NOT NULL,
    `MaintenanceNumber` int NOT NULL,
    CONSTRAINT `PK_Maintenances` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Maintenances_AspNetUsers_ApplicationUserId` FOREIGN KEY (`ApplicationUserId`) REFERENCES `AspNetUsers` (`Id`),
    CONSTRAINT `FK_Maintenances_Devices_DeviceId` FOREIGN KEY (`DeviceId`) REFERENCES `Devices` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_AspNetRoleClaims_RoleId` ON `AspNetRoleClaims` (`RoleId`);

CREATE UNIQUE INDEX `RoleNameIndex` ON `AspNetRoles` (`NormalizedName`);

CREATE INDEX `IX_AspNetUserClaims_UserId` ON `AspNetUserClaims` (`UserId`);

CREATE INDEX `IX_AspNetUserLogins_UserId` ON `AspNetUserLogins` (`UserId`);

CREATE INDEX `IX_AspNetUserRoles_RoleId` ON `AspNetUserRoles` (`RoleId`);

CREATE INDEX `EmailIndex` ON `AspNetUsers` (`NormalizedEmail`);

CREATE UNIQUE INDEX `UserNameIndex` ON `AspNetUsers` (`NormalizedUserName`);

CREATE INDEX `IX_Devices_SectorId` ON `Devices` (`SectorId`);

CREATE INDEX `IX_Maintenances_ApplicationUserId` ON `Maintenances` (`ApplicationUserId`);

CREATE INDEX `IX_Maintenances_DeviceId` ON `Maintenances` (`DeviceId`);

CREATE INDEX `IX_StockManagements_StockId` ON `StockManagements` (`StockId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250130011215_ApplicationUsers', '7.0.13');

COMMIT;

START TRANSACTION;

ALTER TABLE `Maintenances` DROP FOREIGN KEY `FK_Maintenances_Devices_DeviceId`;

ALTER TABLE `Maintenances` MODIFY COLUMN `SimpleDesc` longtext CHARACTER SET utf8mb4 NULL;

ALTER TABLE `Maintenances` MODIFY COLUMN `OpenDate` datetime(6) NULL;

ALTER TABLE `Maintenances` MODIFY COLUMN `MaintenanceNumber` int NULL;

ALTER TABLE `Maintenances` MODIFY COLUMN `DeviceId` int NULL;

ALTER TABLE `Maintenances` MODIFY COLUMN `Description` longtext CHARACTER SET utf8mb4 NULL;

ALTER TABLE `Maintenances` MODIFY COLUMN `CloseDate` datetime(6) NULL;

ALTER TABLE `Maintenances` ADD CONSTRAINT `FK_Maintenances_Devices_DeviceId` FOREIGN KEY (`DeviceId`) REFERENCES `Devices` (`Id`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250130173057_ApplicationUsersChanges', '7.0.13');

COMMIT;

START TRANSACTION;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250131000328_ApplicationUsersChangess', '7.0.13');

COMMIT;

START TRANSACTION;

CREATE TABLE `Logins` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Username` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Password` longtext CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_Logins` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Products` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `ProductFullName` longtext CHARACTER SET utf8mb4 NULL,
    `ProductManufacturer` longtext CHARACTER SET utf8mb4 NULL,
    `CodeBar` longtext CHARACTER SET utf8mb4 NULL,
    `ProductDescription` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Products` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Servers` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `ServerName` longtext CHARACTER SET utf8mb4 NOT NULL,
    `ServerIP` longtext CHARACTER SET utf8mb4 NOT NULL,
    `ServerPassword` longtext CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_Servers` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ServerLogins` (
    `ServerId` int NOT NULL,
    `LoginId` int NOT NULL,
    `CreatedAt` datetime(6) NOT NULL,
    `Permissions` longtext CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_ServerLogins` PRIMARY KEY (`ServerId`, `LoginId`),
    CONSTRAINT `FK_ServerLogins_Logins_LoginId` FOREIGN KEY (`LoginId`) REFERENCES `Logins` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_ServerLogins_Servers_ServerId` FOREIGN KEY (`ServerId`) REFERENCES `Servers` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_ServerLogins_LoginId` ON `ServerLogins` (`LoginId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250202182015_ServerLoginEntities', '7.0.13');

COMMIT;

START TRANSACTION;

CREATE TABLE `MaintenanceProduct` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `MaintenanceId` int NOT NULL,
    `StockId` int NOT NULL,
    `QuantityUsed` int NOT NULL,
    CONSTRAINT `PK_MaintenanceProduct` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_MaintenanceProduct_Maintenances_MaintenanceId` FOREIGN KEY (`MaintenanceId`) REFERENCES `Maintenances` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_MaintenanceProduct_Stocks_StockId` FOREIGN KEY (`StockId`) REFERENCES `Stocks` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_MaintenanceProduct_MaintenanceId` ON `MaintenanceProduct` (`MaintenanceId`);

CREATE INDEX `IX_MaintenanceProduct_StockId` ON `MaintenanceProduct` (`StockId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250204175303_RevertConfigs', '7.0.13');

COMMIT;

START TRANSACTION;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250204204405_MaintenanceStock', '7.0.13');

COMMIT;

START TRANSACTION;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250204213123_MaintenanceStocksUpdate', '7.0.13');

COMMIT;

START TRANSACTION;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250204222340_FixStockAfter', '7.0.13');

COMMIT;

START TRANSACTION;

ALTER TABLE `StockManagements` ADD `MaintenanceId` int NULL;

CREATE INDEX `IX_StockManagements_MaintenanceId` ON `StockManagements` (`MaintenanceId`);

ALTER TABLE `StockManagements` ADD CONSTRAINT `FK_StockManagements_Maintenances_MaintenanceId` FOREIGN KEY (`MaintenanceId`) REFERENCES `Maintenances` (`Id`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250205181548_LogsProducts', '7.0.13');

COMMIT;

START TRANSACTION;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250205193726_ProductsChange', '7.0.13');

COMMIT;

START TRANSACTION;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250205194402_FixMaintenanceRelationship', '7.0.13');

COMMIT;

START TRANSACTION;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250205235714_PublishTestConfig', '7.0.13');

COMMIT;

START TRANSACTION;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250206164238_PublishTestConfigSetMysql', '7.0.13');

COMMIT;

START TRANSACTION;

ALTER TABLE `AspNetUsers` MODIFY COLUMN `OperatorId` int NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250207184910_AlterUserStructure', '7.0.13');

COMMIT;

