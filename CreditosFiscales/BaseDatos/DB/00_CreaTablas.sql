--- Creación de tablas



CREATE TABLE [dbo].[tblControlPagosHead](
	[idArchivo] [bigint] IDENTITY(1,1) NOT NULL,
	[IdProceso] [bigint] NULL,
	[NombreArchivo] [char](11) NULL,
	[IdBanco] [int] NULL,
	[FechaPago] [char](8) NULL,
	[NumRegistros] [int] NULL,
	[Importe] [varchar](18) NULL,
	[FechaRegistro] [datetime] NULL,
	[FechaProceso] [datetime] NULL,
	[IdEstado] [tinyint] NULL,
	[NombreEquipo] [varchar](50) NULL,
	[IdError] [tinyint] null, 

 CONSTRAINT [PK_tblControlPagosHead] PRIMARY KEY CLUSTERED 
(
	[idArchivo] ASC
)WITH (PAD_INDEX = OFF, ALLOW_ROW_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO




CREATE TABLE [dbo].[tblControlPagosDet](
	[IdArchivo] [bigint] NOT NULL,
	[NumLinea] [int] NOT NULL,
	[Consecutivo] [int] NULL,
	[LineaCaptura] [varchar](25) NULL,
	[FechaPago] [char](8) NULL,
	[HoraPago] [char](5) NULL,
	[Importe] [varchar](18) NULL,
	[NumOperacion] [varchar](15) NULL,
	[MedioRecepcion] [tinyint] NULL,
	[Version] [char](3) NULL,
	[TipoPago] [tinyint] NULL,
	[IdEstado] [tinyint] NULL,
	[IdZip] [int] NULL,
	[NombreXML] [varchar](20) NULL,
	[IdError] [tinyint] null, 
 CONSTRAINT [PK_tblControlPagosDet] PRIMARY KEY CLUSTERED 
(
	[IdArchivo] ASC,
	[NumLinea] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



CREATE TABLE [dbo].[tblArchivosZIP](
	[IdZip] [int] IDENTITY(1,1) NOT NULL,
	[NombreZip] [char](18) NULL,
	[NumPagos] [smallint] NULL,
	[Importe] [bigint] NULL,
	[FechaCreacion] [datetime] NULL,
 CONSTRAINT [PK_tblArchivosZIP] PRIMARY KEY CLUSTERED 
(
	[IdZip] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



CREATE TABLE [dbo].[CatEstadoProceso](
	[idEstatus] [tinyint] NOT NULL,
	[Descripcion] [varchar](30) NULL,
 CONSTRAINT [PK_CatEstadoProceso] PRIMARY KEY CLUSTERED 
(
	[idEstatus] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[CatErrorProceso](
	[IdError] [tinyint] NOT NULL,
	[Descripcion] [varchar](200) NOT NULL,
 CONSTRAINT [PK_CatErrorProceso] PRIMARY KEY CLUSTERED 
(
	[IdError] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO






--- LLAVES E INDICES

ALTER TABLE [dbo].[tblControlPagosDet]  WITH CHECK ADD  CONSTRAINT [FK_tblControlPagosDet_tblControlPagosHead] FOREIGN KEY([IdArchivo])
REFERENCES [dbo].[tblControlPagosHead] ([idArchivo])
GO

ALTER TABLE [dbo].[tblControlPagosDet] CHECK CONSTRAINT [FK_tblControlPagosDet_tblControlPagosHead]
GO


alter TABLE tblControlPagosDet ADD idProcesamiento uniqueidentifier NULL 