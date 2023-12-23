USE [MotorTraductor]
GO



/****** Object:  Table [dbo].[CatTipoDocPago]    Script Date: 06/10/2013 13:29:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CatTipoDocPago](
	[IdTipoDocPago] [smallint] NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
 CONSTRAINT [PK_CatTipoDocPago] PRIMARY KEY CLUSTERED 
(
	[IdTipoDocPago] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CatReglasNet]    Script Date: 06/10/2013 13:29:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CatReglasNet](
	[IdReglaNet] [uniqueidentifier] NOT NULL,
	[NombreRegla] [varchar](200) NOT NULL,
	[Descripcion] [varchar](300) NULL,
 CONSTRAINT [PK_CatReglasNet] PRIMARY KEY CLUSTERED 
(
	[IdReglaNet] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CatReglas]    Script Date: 06/10/2013 13:29:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CatReglas](
	[IdRegla] [uniqueidentifier] NOT NULL,
	[Descripcion] [varchar](150) NOT NULL,
	[Regla] [nvarchar](max) NOT NULL,
	[EsValidacion] [bit] NOT NULL,
 CONSTRAINT [PK_CatReglas] PRIMARY KEY CLUSTERED 
(
	[IdRegla] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CatEsquemas]    Script Date: 06/10/2013 13:29:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CatEsquemas](
	[IdEsquema] [smallint] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](150) NOT NULL,
	[Esquema] [nvarchar](max) NOT NULL,
	[TargetNamespace] [varchar](150) NULL,
 CONSTRAINT [PK_CatEsquemas] PRIMARY KEY CLUSTERED 
(
	[IdEsquema] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CatDirectorioReglas]    Script Date: 06/10/2013 13:29:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CatDirectorioReglas](
	[IdDirectorio] [tinyint] NOT NULL,
	[Descripcion] [varchar](100) NULL,
	[Ruta] [varchar](300) NOT NULL,
 CONSTRAINT [PK_CatDirectorioReglas] PRIMARY KEY CLUSTERED 
(
	[IdDirectorio] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CatDireccion]    Script Date: 06/10/2013 13:29:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CatDireccion](
	[Direccion] [tinyint] NOT NULL,
	[Descripcion] [varchar](100) NOT NULL,
 CONSTRAINT [PK_CatDireccion] PRIMARY KEY CLUSTERED 
(
	[Direccion] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CatAplicacion]    Script Date: 06/10/2013 13:29:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CatAplicacion](
	[IdAplicacion] [smallint] NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[IdFoliador] [smallint] NULL,
 CONSTRAINT [PK_CatAplicacion] PRIMARY KEY CLUSTERED 
(
	[IdAplicacion] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CatALR]    Script Date: 06/10/2013 13:29:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CatALR](
	[IdALR] [tinyint] NOT NULL,
	[CveCorta] [nvarchar](15) NULL,
	[Descripcion] [nvarchar](100) NOT NULL,
	[Ciudad] [varchar](50) NULL,
	[Estado] [varchar](50) NULL,
	[NombreTitular] [varchar](70) NULL,
	[Fraccion] [varchar](200) NULL,
	[Prefijo] [char](3) NULL,
	[Sede] [varchar](50) NULL,
	[Circunscripcion] [varchar](50) NULL,
	[NombreSuplente] [varchar](70) NULL,
	[SuplenteActivo] [bit] NULL,
 CONSTRAINT [PK_CatALR] PRIMARY KEY CLUSTERED 
(
	[IdALR] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de la administracion local de recaudación' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CatALR', @level2type=N'COLUMN',@level2name=N'IdALR'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Clave corta de la administración local de recaudación' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CatALR', @level2type=N'COLUMN',@level2name=N'CveCorta'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre de la administración local de recaudación' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CatALR', @level2type=N'COLUMN',@level2name=N'Descripcion'
GO
/****** Object:  Table [dbo].[TblDatosGenerales]    Script Date: 06/10/2013 13:29:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TblDatosGenerales](
	[IdPeticion] [uniqueidentifier] NOT NULL,
	[RFC] [varchar](13) NULL,
	[ClaveAlr] [smallint] NULL,
	[LineaCaptura] [varchar](20) NULL,
	[ImporteTotal] [bigint] NULL,
	[FechaVigencia] [smalldatetime] NULL,
 CONSTRAINT [PK_TblDatosGenerales] PRIMARY KEY CLUSTERED 
(
	[IdPeticion] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TblBitacoraProcesamiento]    Script Date: 06/10/2013 13:29:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblBitacoraProcesamiento](
	[IdAplicacion] [smallint] NOT NULL,
	[IdTipoDocPago] [smallint] NOT NULL,
	[IdProcesamiento] [uniqueidentifier] NOT NULL,
	[FechaRegistro] [datetime] NOT NULL,
	[Paso] [tinyint] NOT NULL,
	[Observaciones] [nvarchar](4000) NULL,
	[Mensaje] [xml] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblFoliador]    Script Date: 06/10/2013 13:29:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblFoliador](
	[IdFoliador] [smallint] NOT NULL,
	[Anio] [smallint] NOT NULL,
	[IdALR] [tinyint] NOT NULL,
	[Folio] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblBitacoraErrores]    Script Date: 06/10/2013 13:29:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TblBitacoraErrores](
	[IdAplicacion] [smallint] NOT NULL,
	[IdTipoDocPago] [smallint] NOT NULL,
	[IdProcesamiento] [uniqueidentifier] NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[Errores] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TblAgrupador]    Script Date: 06/10/2013 13:29:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblAgrupador](
	[IdAgrupador] [int] IDENTITY(1,1) NOT NULL,
	[IdPeticion] [uniqueidentifier] NULL,
	[ValorAgrupador] [nvarchar](max) NULL,
	[Total] [decimal](18, 0) NULL,
 CONSTRAINT [PK_TblAgrupador] PRIMARY KEY CLUSTERED 
(
	[IdAgrupador] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblAdmonReglas]    Script Date: 06/10/2013 13:29:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblAdmonReglas](
	[IdRegla] [uniqueidentifier] NOT NULL,
	[IdAplicacion] [smallint] NOT NULL,
	[IdTipoDocPago] [smallint] NOT NULL,
	[Secuencia] [tinyint] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblAdmonEsquemas]    Script Date: 06/10/2013 13:29:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblAdmonEsquemas](
	[IdEsquema] [smallint] NOT NULL,
	[IdAplicacion] [smallint] NOT NULL,
	[IdTipoDocPago] [smallint] NOT NULL,
	[Direccion] [tinyint] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblConceptos]    Script Date: 06/10/2013 13:29:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblConceptos](
	[IdPeticion] [uniqueidentifier] NOT NULL,
	[IdConcepto] [uniqueidentifier] NOT NULL,
	[IdAgrupador] [int] NULL,
	[Clave] [int] NULL,
	[Ejercicio] [smallint] NULL,
	[ClavePeriodicidad] [tinyint] NULL,
	[ClavePeriodo] [tinyint] NULL,
	[FechaCausacion] [date] NULL,
 CONSTRAINT [PK_TblConceptos] PRIMARY KEY CLUSTERED 
(
	[IdConcepto] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblTransacciones]    Script Date: 06/10/2013 13:29:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TblTransacciones](
	[IdConcepto] [uniqueidentifier] NOT NULL,
	[Clave] [int] NULL,
	[Descripcion] [varchar](100) NULL,
	[Valor] [bigint] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Default [DF_CatALR_SuplenteActivo]    Script Date: 06/10/2013 13:29:31 ******/
ALTER TABLE [dbo].[CatALR] ADD  CONSTRAINT [DF_CatALR_SuplenteActivo]  DEFAULT ((0)) FOR [SuplenteActivo]
GO
/****** Object:  Default [DF_CatReglas_IdRegla]    Script Date: 06/10/2013 13:29:31 ******/
ALTER TABLE [dbo].[CatReglas] ADD  CONSTRAINT [DF_CatReglas_IdRegla]  DEFAULT (newid()) FOR [IdRegla]
GO
/****** Object:  Default [DF_TblReglas_EsValidacion]    Script Date: 06/10/2013 13:29:31 ******/
ALTER TABLE [dbo].[CatReglas] ADD  CONSTRAINT [DF_TblReglas_EsValidacion]  DEFAULT ((0)) FOR [EsValidacion]
GO
/****** Object:  Default [DF_CatReglasNet_IdReglaNet]    Script Date: 06/10/2013 13:29:31 ******/
ALTER TABLE [dbo].[CatReglasNet] ADD  CONSTRAINT [DF_CatReglasNet_IdReglaNet]  DEFAULT (newid()) FOR [IdReglaNet]
GO
/****** Object:  Default [DF_TblBitacoraErrores_Fecha]    Script Date: 06/10/2013 13:29:31 ******/
ALTER TABLE [dbo].[TblBitacoraErrores] ADD  CONSTRAINT [DF_TblBitacoraErrores_Fecha]  DEFAULT (getdate()) FOR [Fecha]
GO
/****** Object:  Default [DF_TblBitacoraProcesamiento_FechaRegistro]    Script Date: 06/10/2013 13:29:31 ******/
ALTER TABLE [dbo].[TblBitacoraProcesamiento] ADD  CONSTRAINT [DF_TblBitacoraProcesamiento_FechaRegistro]  DEFAULT (getdate()) FOR [FechaRegistro]
GO
/****** Object:  ForeignKey [FK_TblAdmonEsquemas_CatAplicacion]    Script Date: 06/10/2013 13:29:31 ******/
ALTER TABLE [dbo].[TblAdmonEsquemas]  WITH CHECK ADD  CONSTRAINT [FK_TblAdmonEsquemas_CatAplicacion] FOREIGN KEY([IdAplicacion])
REFERENCES [dbo].[CatAplicacion] ([IdAplicacion])
GO
ALTER TABLE [dbo].[TblAdmonEsquemas] CHECK CONSTRAINT [FK_TblAdmonEsquemas_CatAplicacion]
GO
/****** Object:  ForeignKey [FK_TblAdmonEsquemas_CatEsquemas]    Script Date: 06/10/2013 13:29:31 ******/
ALTER TABLE [dbo].[TblAdmonEsquemas]  WITH CHECK ADD  CONSTRAINT [FK_TblAdmonEsquemas_CatEsquemas] FOREIGN KEY([IdEsquema])
REFERENCES [dbo].[CatEsquemas] ([IdEsquema])
GO
ALTER TABLE [dbo].[TblAdmonEsquemas] CHECK CONSTRAINT [FK_TblAdmonEsquemas_CatEsquemas]
GO
/****** Object:  ForeignKey [FK_TblAdmonEsquemas_CatTipoDocPago]    Script Date: 06/10/2013 13:29:31 ******/
ALTER TABLE [dbo].[TblAdmonEsquemas]  WITH CHECK ADD  CONSTRAINT [FK_TblAdmonEsquemas_CatTipoDocPago] FOREIGN KEY([IdTipoDocPago])
REFERENCES [dbo].[CatTipoDocPago] ([IdTipoDocPago])
GO
ALTER TABLE [dbo].[TblAdmonEsquemas] CHECK CONSTRAINT [FK_TblAdmonEsquemas_CatTipoDocPago]
GO
/****** Object:  ForeignKey [FK_TblAdmonReglas_CatAplicacion]    Script Date: 06/10/2013 13:29:31 ******/
ALTER TABLE [dbo].[TblAdmonReglas]  WITH CHECK ADD  CONSTRAINT [FK_TblAdmonReglas_CatAplicacion] FOREIGN KEY([IdAplicacion])
REFERENCES [dbo].[CatAplicacion] ([IdAplicacion])
GO
ALTER TABLE [dbo].[TblAdmonReglas] CHECK CONSTRAINT [FK_TblAdmonReglas_CatAplicacion]
GO
/****** Object:  ForeignKey [FK_TblAdmonReglas_CatTipoDocPago]    Script Date: 06/10/2013 13:29:31 ******/
ALTER TABLE [dbo].[TblAdmonReglas]  WITH CHECK ADD  CONSTRAINT [FK_TblAdmonReglas_CatTipoDocPago] FOREIGN KEY([IdTipoDocPago])
REFERENCES [dbo].[CatTipoDocPago] ([IdTipoDocPago])
GO
ALTER TABLE [dbo].[TblAdmonReglas] CHECK CONSTRAINT [FK_TblAdmonReglas_CatTipoDocPago]
GO
/****** Object:  ForeignKey [FK_TblAgrupador_TblDatosGenerales]    Script Date: 06/10/2013 13:29:31 ******/
ALTER TABLE [dbo].[TblAgrupador]  WITH CHECK ADD  CONSTRAINT [FK_TblAgrupador_TblDatosGenerales] FOREIGN KEY([IdPeticion])
REFERENCES [dbo].[TblDatosGenerales] ([IdPeticion])
GO
ALTER TABLE [dbo].[TblAgrupador] CHECK CONSTRAINT [FK_TblAgrupador_TblDatosGenerales]
GO
/****** Object:  ForeignKey [FK_TblBitacoraErrores_CatAplicacion]    Script Date: 06/10/2013 13:29:31 ******/
ALTER TABLE [dbo].[TblBitacoraErrores]  WITH CHECK ADD  CONSTRAINT [FK_TblBitacoraErrores_CatAplicacion] FOREIGN KEY([IdAplicacion])
REFERENCES [dbo].[CatAplicacion] ([IdAplicacion])
GO
ALTER TABLE [dbo].[TblBitacoraErrores] CHECK CONSTRAINT [FK_TblBitacoraErrores_CatAplicacion]
GO
/****** Object:  ForeignKey [FK_TblBitacoraErrores_CatTipoDocPago]    Script Date: 06/10/2013 13:29:31 ******/
ALTER TABLE [dbo].[TblBitacoraErrores]  WITH CHECK ADD  CONSTRAINT [FK_TblBitacoraErrores_CatTipoDocPago] FOREIGN KEY([IdTipoDocPago])
REFERENCES [dbo].[CatTipoDocPago] ([IdTipoDocPago])
GO
ALTER TABLE [dbo].[TblBitacoraErrores] CHECK CONSTRAINT [FK_TblBitacoraErrores_CatTipoDocPago]
GO
/****** Object:  ForeignKey [FK_TblConceptos_TblAgrupador]    Script Date: 06/10/2013 13:29:31 ******/
ALTER TABLE [dbo].[TblConceptos]  WITH CHECK ADD  CONSTRAINT [FK_TblConceptos_TblAgrupador] FOREIGN KEY([IdAgrupador])
REFERENCES [dbo].[TblAgrupador] ([IdAgrupador])
GO
ALTER TABLE [dbo].[TblConceptos] CHECK CONSTRAINT [FK_TblConceptos_TblAgrupador]
GO
/****** Object:  ForeignKey [FK_TblFoliador_CatALR]    Script Date: 06/10/2013 13:29:31 ******/
ALTER TABLE [dbo].[TblFoliador]  WITH CHECK ADD  CONSTRAINT [FK_TblFoliador_CatALR] FOREIGN KEY([IdALR])
REFERENCES [dbo].[CatALR] ([IdALR])
GO
ALTER TABLE [dbo].[TblFoliador] CHECK CONSTRAINT [FK_TblFoliador_CatALR]
GO
/****** Object:  ForeignKey [FK_TblTransacciones_TblConceptos]    Script Date: 06/10/2013 13:29:31 ******/
ALTER TABLE [dbo].[TblTransacciones]  WITH CHECK ADD  CONSTRAINT [FK_TblTransacciones_TblConceptos] FOREIGN KEY([IdConcepto])
REFERENCES [dbo].[TblConceptos] ([IdConcepto])
GO
ALTER TABLE [dbo].[TblTransacciones] CHECK CONSTRAINT [FK_TblTransacciones_TblConceptos]
GO
