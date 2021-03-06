USE [master]
GO
/****** Object:  Database [amu_Flow]    Script Date: 2018-03-20 02:12:03 PM ******/
CREATE DATABASE [amu_Flow]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'amu_Flow', FILENAME = N'D:\DataFiles\amu_Flow.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'amu_Flow_log', FILENAME = N'D:\DataFiles\amu_Flow_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [amu_Flow] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [amu_Flow].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [amu_Flow] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [amu_Flow] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [amu_Flow] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [amu_Flow] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [amu_Flow] SET ARITHABORT OFF 
GO
ALTER DATABASE [amu_Flow] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [amu_Flow] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [amu_Flow] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [amu_Flow] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [amu_Flow] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [amu_Flow] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [amu_Flow] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [amu_Flow] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [amu_Flow] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [amu_Flow] SET  DISABLE_BROKER 
GO
ALTER DATABASE [amu_Flow] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [amu_Flow] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [amu_Flow] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [amu_Flow] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [amu_Flow] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [amu_Flow] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [amu_Flow] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [amu_Flow] SET RECOVERY FULL 
GO
ALTER DATABASE [amu_Flow] SET  MULTI_USER 
GO
ALTER DATABASE [amu_Flow] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [amu_Flow] SET DB_CHAINING OFF 
GO
ALTER DATABASE [amu_Flow] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [amu_Flow] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [amu_Flow] SET DELAYED_DURABILITY = DISABLED 
GO
USE [amu_Flow]
GO
/****** Object:  User [Trojan]    Script Date: 2018-03-20 02:12:03 PM ******/
CREATE USER [Trojan] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [tl_User]    Script Date: 2018-03-20 02:12:03 PM ******/
CREATE USER [tl_User] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [qa_tl_user]    Script Date: 2018-03-20 02:12:03 PM ******/
CREATE USER [qa_tl_user] FOR LOGIN [qa_tl_user] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [ProdUser]    Script Date: 2018-03-20 02:12:03 PM ******/
CREATE USER [ProdUser] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[ProdUser]
GO
ALTER ROLE [db_owner] ADD MEMBER [qa_tl_user]
GO
ALTER ROLE [db_datareader] ADD MEMBER [qa_tl_user]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [qa_tl_user]
GO
/****** Object:  Schema [ProdUser]    Script Date: 2018-03-20 02:12:04 PM ******/
CREATE SCHEMA [ProdUser]
GO
/****** Object:  Table [dbo].[ActivityLog]    Script Date: 2018-03-20 02:12:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ActivityLog](
	[id] [uniqueidentifier] NOT NULL,
	[date] [datetime] NULL,
	[MemberID] [uniqueidentifier] NULL,
	[action] [varchar](50) NULL,
 CONSTRAINT [PK_ActivityLog] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Members]    Script Date: 2018-03-20 02:12:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Members](
	[id] [uniqueidentifier] NOT NULL,
	[name] [varchar](255) NULL,
	[surname] [varchar](255) NULL,
	[email] [varchar](255) NULL,
 CONSTRAINT [PK_Members] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[domain_grouping]    Script Date: 2018-03-20 02:12:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[domain_grouping]
AS
SELECT COUNT(Domain) AS numberOfDomains, Domain
FROM  (SELECT SUBSTRING(email, CHARINDEX('@', email) + 1, LEN(email)) AS Domain
        FROM   dbo.Members) AS A
GROUP BY Domain

GO
/****** Object:  View [dbo].[members_list]    Script Date: 2018-03-20 02:12:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[members_list]
AS
SELECT name, surname, email, SUBSTRING(email, 1, CHARINDEX('@', email) - 1) AS username, SUBSTRING(email, CHARINDEX('@', email) + 1, LEN(email)) AS domain
FROM  dbo.Members

GO
/****** Object:  StoredProcedure [dbo].[spActivityLog]    Script Date: 2018-03-20 02:12:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Aubrey Nkanyani>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[spActivityLog]
	@id uniqueidentifier,@date datetime, @MemberID uniqueidentifier, @action varchar(50)
AS
BEGIN
	INSERT INTO [dbo].[ActivityLog] (id,date,MemberID,action)
	VALUES  (@id,@date,@MemberID,@action)
END


GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "A"
            Begin Extent = 
               Top = 15
               Left = 96
               Bottom = 195
               Right = 424
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 600
         Width = 600
         Width = 600
         Width = 600
         Width = 600
         Width = 600
         Width = 600
         Width = 600
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'domain_grouping'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'domain_grouping'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Members"
            Begin Extent = 
               Top = 15
               Left = 96
               Bottom = 324
               Right = 424
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 600
         Width = 600
         Width = 600
         Width = 600
         Width = 600
         Width = 600
         Width = 600
         Width = 600
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'members_list'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'members_list'
GO
USE [master]
GO
ALTER DATABASE [amu_Flow] SET  READ_WRITE 
GO
