USE motortraductor
go
--cataplicacion
if  (select count(*) from cataplicacion where idaplicacion=5 )=0
 begin
  insert into cataplicacion select 5,'MAT',44,5 from cataplicacion where idaplicacion=2
 end
--CatConceptoEquivalencia
 if  (select count(*) from CatConceptoEquivalencia where idaplicacion=5 )=0
 begin
  insert into CatConceptoEquivalencia select 5,ConceptoOrigen,DescripcionOrigen,ConceptoDyP,Activo from CatConceptoEquivalencia 
  where idaplicacion=2 
 end
--CatConceptosOriginal
 if  (select count(*) from CatConceptosOriginal where idaplicacion=5 )=0
 begin
  insert into CatConceptosOriginal select clave,5,Descripcion,Activo from CatConceptosOriginal 
  where idaplicacion=2 
 end
 --CatFormatoImpresionEquivalencia
 if  (select count(*) from CatFormatoImpresionEquivalencia where idaplicacion=5 and IdTipoDocumento=10 )=0
 begin
  insert into CatFormatoImpresionEquivalencia select 5,IdTipoDocumento,IdFormatoImpresion,Descripcion,XPathEvaluacion,ValorEvaluacion from CatFormatoImpresionEquivalencia 
  where idaplicacion=2 and IdTipoDocumento=10
 end
--CatOrdenamientoTransaccion
 if  (select count(*) from CatOrdenamientoTransaccion where idaplicacion=5 and idtipodocumento=10 )=0
 begin
 DECLARE @Id int,
	@IdAplicacion smallint,
	@IdTipoDocumento smallint,
	@Ordenamiento varchar(100),
	@Secuencia smallint
	
	DECLARE cCatOrdTra CURSOR FOR
    select * from CatOrdenamientoTransaccion where idaplicacion=2 and idtipodocumento=10 
    OPEN cCatOrdTra
    
    FETCH cCatOrdTra INTO    @Id, @IdAplicacion, @IdTipoDocumento, 
			@Ordenamiento, @Secuencia
    WHILE (@@FETCH_STATUS = 0 )
    begin
     
        SET @Id =(SELECT TOP(1)Id+1 from CatOrdenamientoTransaccion order by id desc)
        INSERT INTO CatOrdenamientoTransaccion Values( @Id, 5, @IdTipoDocumento, 
			@Ordenamiento, @Secuencia)
			
     FETCH cCatOrdTra INTO    @Id, @IdAplicacion, @IdTipoDocumento, 
	  @Ordenamiento, @Secuencia
     END
CLOSE cCatOrdTra
DEALLOCATE cCatOrdTra
end
--CatPeriodicidadEquivalencia
 if  (select count(*) from CatPeriodicidadEquivalencia where idaplicacion=5 )=0
 begin
  insert into CatPeriodicidadEquivalencia select 5,PeriodicidadOrigen,DescripcionOrigen,IdPeriodicidadDyP,Activo
  from CatPeriodicidadEquivalencia 
  where idaplicacion=2 
 end
--CatPeriodoEquivalencia
if  (select count(*) from CatPeriodoEquivalencia where idaplicacion=5 )=0
 begin
  insert into CatPeriodoEquivalencia select 5,PeriodicidadOrigen,PeriodoOrigen,DescripcionOrigen,IdPeriodoDyP,
  IdPeriodicidadDyP,Activo
  from CatPeriodoEquivalencia 
  where idaplicacion=2 
 end 
--CatReglasEquivalencia
Go
 if  (select count(*) from CatReglasEquivalencia where idaplicacion=5 and idtipodocumento=10  )=0
 begin
 DECLARE    @Id int,
            @IdAplicacion   smallint  
           ,@IdTipoDocumento smallint  
           ,@Descripcion varchar(300)  
           ,@IdTipoObjeto tinyint  
           ,@ValorObjeto varchar(100)  
           ,@ValorRetorno varchar(100)  
           ,@XPathEvaluacion varchar (max)  
           ,@ValorEvaluacion varchar(100)  
           ,@Activo bit  
           ,@Secuencia smallint  
           ,@IdPadre int  
	
	DECLARE cCatRegEq CURSOR FOR
    select * from CatReglasEquivalencia where idaplicacion=2 and idtipodocumento=10 
    OPEN cCatRegEq
    
    FETCH cCatRegEq INTO    @Id, @IdAplicacion, @IdTipoDocumento, 
			@Descripcion, @IdTipoObjeto,@ValorObjeto,@ValorRetorno,@XPathEvaluacion,@ValorEvaluacion,
			@Activo,@Secuencia,@IdPadre
    WHILE (@@FETCH_STATUS = 0 )
    begin
    EXECUTE pInsertaReglaEquivalencia 5, @IdTipoDocumento, 
			@Descripcion, @IdTipoObjeto,@ValorObjeto,@ValorRetorno,@XPathEvaluacion,@ValorEvaluacion,
			@Activo,@Secuencia,@IdPadre  			
      FETCH cCatRegEq INTO    @Id, @IdAplicacion, @IdTipoDocumento, 
			@Descripcion, @IdTipoObjeto,@ValorObjeto,@ValorRetorno,@XPathEvaluacion,@ValorEvaluacion,
			@Activo,@Secuencia,@IdPadre
     END
CLOSE cCatRegEq
DEALLOCATE cCatRegEq
end
--CatTransaccion
 if  (select count(*) from CatTransaccion where idaplicacion=5 and idtipodocumento=10 )=0
 begin
  insert into CatTransaccion select CveTransaccion,Descripcion,IdTipoTransaccion,TipoTransaccion,
  IdTipoDocumento,5,EsObligatorio,Activo
  from CatTransaccion 
  where idaplicacion=2 and idtipodocumento=10
 end
--TblAdmonEsquemas
if  (select count(*) from TblAdmonEsquemas where idaplicacion=5 and idtipodocumento=10)=0
 begin

  DECLARE @idEsquema as INT
  insert CatEsquemas (Descripcion,Esquema)
   values(
  'Esquema entrada para Pagos en una sola exhibición.Con convenio',
  '<xs:schema xmlns:b="http://schemas.microsoft.com/BizTalk/2003" xmlns:ns0="http://SAT.CreditosFiscales.Traductor" xmlns:xs="http://www.w3.org/2001/XMLSchema" attributeFormDefault="unqualified" elementFormDefault="qualified">   <xs:element name="SolicitudCreditosFiscales">     <xs:annotation>       <xs:appinfo>         <b:recordInfo xmlns:b="http://schemas.microsoft.com/BizTalk/2003" rootTypeName="SolicitudCreditosFiscales" />       </xs:appinfo>     </xs:annotation>     <xs:complexType>       <xs:sequence>         <xs:element minOccurs="1" maxOccurs="1" name="Encabezado">           <xs:complexType>             <xs:sequence> <xs:element minOccurs="1" maxOccurs="1" name="TipoDocumento">   <xs:annotation>     <xs:appinfo>       <b:fieldInfo notes="Identificador del Tipo de Documento, posibles valores (10-Pago en una sola exhibición. 21-Pagos en parcialidades o diferido (pago inicial). 22-Pagos en parcialidades o diferido (autorización). 23-Pagos a cuenta (abonos o intervenciones). 30-Movimientos Contables. 40-Movimientos de rectificación)" />     </xs:appinfo>   </xs:annotation>   <xs:simpleType>     <xs:restriction base="xs:int">       <xs:pattern value="^\d{1,2}$" />     </xs:restriction>   </xs:simpleType> </xs:element> <xs:element minOccurs="1" maxOccurs="1" name="DescripcionTipoDocumento">   <xs:annotation>     <xs:appinfo>       <b:fieldInfo notes="La descripción del tipo de documento a procesar." />     </xs:appinfo>   </xs:annotation>   <xs:simpleType>     <xs:restriction base="xs:string">       <xs:maxLength value="50" />     </xs:restriction>   </xs:simpleType> </xs:element> <xs:element minOccurs="1" maxOccurs="1" name="Aplicacion" type="xs:int">   <xs:annotation>     <xs:appinfo>       <b:fieldInfo notes="Identificador de la aplicación que solicita el proceso. (1. SIR, 2. ARCA)" />     </xs:appinfo>   </xs:annotation> </xs:element> <xs:element minOccurs="0" maxOccurs="1" name="TipoOperacion" type="xs:string" />             </xs:sequence>           </xs:complexType>         </xs:element>         <xs:element minOccurs="1" maxOccurs="1" name="Mensaje">           <xs:complexType>             <xs:sequence> <xs:element minOccurs="1" maxOccurs="1" name="DatosGenerales">   <xs:complexType>     <xs:sequence>       <xs:element minOccurs="0" maxOccurs="1" name="IdPersona">         <xs:simpleType>           <xs:restriction base="xs:string" />         </xs:simpleType>       </xs:element>       <xs:element minOccurs="1" maxOccurs="1" name="RFC">         <xs:annotation>           <xs:appinfo>             <b:fieldInfo notes="RFC Actualizado del contribuyente" />           </xs:appinfo>         </xs:annotation>         <xs:simpleType>           <xs:restriction base="xs:string" />         </xs:simpleType>       </xs:element>       <xs:element minOccurs="0" maxOccurs="1" name="Nombre">         <xs:simpleType>           <xs:restriction base="xs:string">             <xs:maxLength value="250" />           </xs:restriction>         </xs:simpleType>       </xs:element>       <xs:element minOccurs="0" maxOccurs="1" name="ApellidoPaterno">         <xs:simpleType>           <xs:restriction base="xs:string">             <xs:maxLength value="60" />           </xs:restriction>         </xs:simpleType>       </xs:element>       <xs:element minOccurs="0" maxOccurs="1" name="ApellidoMaterno">         <xs:simpleType>           <xs:restriction base="xs:string">             <xs:maxLength value="60" />           </xs:restriction>         </xs:simpleType>       </xs:element>       <xs:element minOccurs="0" maxOccurs="1" name="RazonSocial">         <xs:simpleType>           <xs:restriction base="xs:string">             <xs:maxLength value="250" />           </xs:restriction>         </xs:simpleType>       </xs:element>       <xs:element minOccurs="1" maxOccurs="1" name="ALR">         <xs:annotation>           <xs:appinfo>             <b:fieldInfo notes="ALR del contribuyente" />           </xs:appinfo>         </xs:annotation>         <xs:simpleType>           <xs:restriction base="xs:int">             <xs:maxInclusive value="255" />           </xs:restriction>         </xs:simpleType>       </xs:element>       <xs:element minOccurs="1" maxOccurs="1" name="BOID">         <xs:annotation>           <xs:appinfo>             <b:fieldInfo notes="Indentificador único del contribuyente en IDC" />           </xs:appinfo>         </xs:annotation>         <xs:simpleType>           <xs:restriction base="xs:string">             <xs:maxLength value="50" />           </xs:restriction>         </xs:simpleType>       </xs:element>       <xs:element minOccurs="1" maxOccurs="1" name="LineaCaptura">         <xs:complexType>           <xs:sequence>             <xs:element minOccurs="1" maxOccurs="1" name="Importe">               <xs:annotation>                 <xs:appinfo>                   <b:fieldInfo notes="Monto por el cual se generará la línea de captura" />                 </xs:appinfo>               </xs:annotation>               <xs:simpleType>                 <xs:restriction base="xs:long">                   <xs:pattern value="^\d{1,14}$" />                 </xs:restriction>               </xs:simpleType>             </xs:element>             <xs:element minOccurs="1" maxOccurs="1" name="FechaVigencia">               <xs:annotation>                 <xs:appinfo>                   <b:fieldInfo notes="Fecha que indica el término de la vigencia de la línea" />                 </xs:appinfo>               </xs:annotation>               <xs:simpleType>                 <xs:restriction base="xs:string">                   <xs:pattern value="(0[1-9]|[12][0-9]|3[01])[/.](0[1-9]|1[012])[/.](19|20)\d\d" />                 </xs:restriction>               </xs:simpleType>             </xs:element>             <xs:element minOccurs="1" maxOccurs="1" name="PagoObligadoInternet">               <xs:annotation>                 <xs:appinfo>                   <b:fieldInfo notes="2- No Obligado (Personas Físicas), 4-Obligado (Personas Morales)" />                 </xs:appinfo>               </xs:annotation>               <xs:simpleType>                 <xs:restriction base="xs:int">                   <xs:pattern value="(^(2)$)|(^(4)$)" />                 </xs:restriction>               </xs:simpleType>             </xs:element>             <xs:element minOccurs="0" maxOccurs="1" name="Tipo" type="xs:int" />           </xs:sequence>         </xs:complexType>       </xs:element>       <xs:element default="0" name="DeudorPuro">         <xs:simpleType>           <xs:restriction base="xs:int">             <xs:enumeration value="0" />             <xs:enumeration value="1" />           </xs:restriction>         </xs:simpleType>       </xs:element>       <xs:element minOccurs="0" maxOccurs="1" name="Observaciones">         <xs:simpleType>           <xs:restriction base="xs:string">             <xs:maxLength value="255" />           </xs:restriction>         </xs:simpleType>       </xs:element>       <xs:element minOccurs="0" maxOccurs="1" name="Convenio">         <xs:complexType>           <xs:sequence>             <xs:element minOccurs="1" maxOccurs="1" name="IdConvenio">               <xs:annotation>                 <xs:appinfo>                   <b:fieldInfo notes="Id del convenio" />                 </xs:appinfo>               </xs:annotation>               <xs:simpleType>                 <xs:restriction base="xs:string" />               </xs:simpleType>             </xs:element>             <xs:element minOccurs="1" maxOccurs="1" name="TipoConvenio">               <xs:annotation>                 <xs:appinfo>                   <b:fieldInfo notes="Tipo de convenio" />                 </xs:appinfo>               </xs:annotation>               <xs:simpleType>                 <xs:restriction base="xs:int" />               </xs:simpleType>             </xs:element>             <xs:element minOccurs="1" maxOccurs="1" name="NumeroConvenio">               <xs:annotation>                 <xs:appinfo>                   <b:fieldInfo notes="Numero de convenio" />                 </xs:appinfo>               </xs:annotation>               <xs:simpleType>                 <xs:restriction base="xs:string" />               </xs:simpleType>             </xs:element>             <xs:element minOccurs="1" maxOccurs="1" name="ImporteSaldoInsolutoInicial">               <xs:annotation>                 <xs:appinfo>                   <b:fieldInfo notes="Importe inicial a diferir o parcializar." />                 </xs:appinfo>               </xs:annotation>               <xs:simpleType>                 <xs:restriction base="xs:long">                   <xs:pattern value="^\d{1,14}$" />                 </xs:restriction>               </xs:simpleType>             </xs:element>             <xs:element minOccurs="1" maxOccurs="1" name="ImporteSaldoInsoluto">               <xs:annotation>                 <xs:appinfo>                   <b:fieldInfo notes="Saldo insoluto del pago a plazos." />                 </xs:appinfo>               </xs:annotation>               <xs:simpleType>                 <xs:restriction base="xs:long">                   <xs:pattern value="^\d{1,14}$" />                 </xs:restriction>               </xs:simpleType>             </xs:element>           </xs:sequence>         </xs:complexType>       </xs:element>     </xs:sequence>   </xs:complexType> </xs:element> <xs:element minOccurs="1" maxOccurs="1" name="ResolucionesDeterminante">   <xs:complexType>     <xs:sequence>       <xs:element minOccurs="1" maxOccurs="unbounded" name="ResolucionDeterminante">         <xs:complexType>           <xs:sequence>             <xs:element minOccurs="1" maxOccurs="1" name="NumeroResolucion">               <xs:annotation>                 <xs:appinfo>                   <b:fieldInfo notes="Resolución determinante" />                 </xs:appinfo>               </xs:annotation>               <xs:simpleType>                 <xs:restriction base="xs:string">                   <xs:maxLength value="50" />                   <xs:minLength value="1" />                 </xs:restriction>               </xs:simpleType>             </xs:element>             <xs:element minOccurs="0" maxOccurs="1" name="IdResolucion">               <xs:annotation>                 <xs:appinfo>                   <b:fieldInfo notes="Id de la Resolución" />                 </xs:appinfo>               </xs:annotation>               <xs:simpleType>                 <xs:restriction base="xs:string" />               </xs:simpleType>             </xs:element>             <xs:element minOccurs="1" maxOccurs="1" name="Fecha">               <xs:annotation>                 <xs:appinfo>                   <b:fieldInfo notes="Fecha de la resolución" />                 </xs:appinfo>               </xs:annotation>               <xs:simpleType>                 <xs:restriction base="xs:string">                   <xs:pattern value="(0[1-9]|[12][0-9]|3[01])[/.](0[1-9]|1[012])[/.](19|20)\d\d" />                 </xs:restriction>               </xs:simpleType>             </xs:element>             <xs:element minOccurs="1" maxOccurs="1" name="AutoridadId">               <xs:annotation>                 <xs:appinfo>                   <b:fieldInfo notes="Autoridad que impone la resolución" />                 </xs:appinfo>               </xs:annotation>               <xs:simpleType>                 <xs:restriction base="xs:int" />               </xs:simpleType>             </xs:element>             <xs:element minOccurs="1" maxOccurs="1" name="ALR">               <xs:annotation>                 <xs:appinfo>                   <b:fieldInfo notes="ALR donde se generó la resolución" />                 </xs:appinfo>               </xs:annotation>               <xs:simpleType>                 <xs:restriction base="xs:int">                   <xs:maxInclusive value="255" />                 </xs:restriction>               </xs:simpleType>             </xs:element>             <xs:element minOccurs="1" maxOccurs="1" name="RFC">               <xs:annotation>                 <xs:appinfo>                   <b:fieldInfo notes="RFC asociado a la resolución" />                 </xs:appinfo>               </xs:annotation>               <xs:simpleType>                 <xs:restriction base="xs:string" />               </xs:simpleType>             </xs:element>             <xs:element minOccurs="1" maxOccurs="1" name="SaldoInformativo">               <xs:simpleType>                 <xs:restriction base="xs:decimal" />               </xs:simpleType>             </xs:element>             <xs:element minOccurs="1" maxOccurs="1" name="FormaPago">               <xs:simpleType>                 <xs:restriction base="xs:string">                   <xs:pattern value="^\d{6}-\d{4}" />                 </xs:restriction>               </xs:simpleType>             </xs:element>             <xs:element minOccurs="1" maxOccurs="1" name="Conceptos">               <xs:complexType>                 <xs:sequence>                   <xs:element minOccurs="1" maxOccurs="unbounded" name="Concepto">                     <xs:complexType>                       <xs:sequence>                         <xs:element minOccurs="1" maxOccurs="1" name="Clave">                           <xs:annotation>                             <xs:appinfo>                               <b:fieldInfo notes="Clave del concepto ley" />                             </xs:appinfo>                           </xs:annotation>                           <xs:simpleType>                             <xs:restriction base="xs:string">                               <xs:maxLength value="6" />                             </xs:restriction>                           </xs:simpleType>                         </xs:element>                         <xs:element minOccurs="0" maxOccurs="1" name="IdConcepto">                           <xs:annotation>                             <xs:appinfo>                               <b:fieldInfo notes="ID del concepto" />                             </xs:appinfo>                           </xs:annotation>                           <xs:simpleType>                             <xs:restriction base="xs:string" />                           </xs:simpleType>                         </xs:element>                         <xs:element minOccurs="0" maxOccurs="1" name="Ejercicio">                           <xs:simpleType>                             <xs:restriction base="xs:int">                               <xs:minInclusive value="1900" />                               <xs:maxInclusive value="2100" />                             </xs:restriction>                           </xs:simpleType>                         </xs:element>                         <xs:element minOccurs="1" maxOccurs="1" name="PeriodicidadId">                           <xs:annotation>                             <xs:appinfo>                               <b:fieldInfo notes="1-Mensual. 2-Bimestral. 3-Cuatrimestral. 4-Trimestral. 5-Semestral. 9. Sin Periodo" />                             </xs:appinfo>                           </xs:annotation>                           <xs:simpleType>                             <xs:restriction base="xs:int">                               <xs:maxInclusive value="255" />                             </xs:restriction>                           </xs:simpleType>                         </xs:element>                         <xs:element minOccurs="1" maxOccurs="1" name="PeriodoId">                           <xs:annotation>                             <xs:appinfo>                               <b:fieldInfo notes="1 Enero&#xD;&#xA;2 Febrero&#xD;&#xA;3 Marzo&#xD;&#xA;4 Abril&#xD;&#xA;5 Mayo&#xD;&#xA;6 Junio&#xD;&#xA;7 Julio&#xD;&#xA;8 Agosto&#xD;&#xA;9 Septiembre&#xD;&#xA;10 Octubre&#xD;&#xA;11 Noviembre&#xD;&#xA;12 Diciembre&#xD;&#xA;13 Enero-Febrero&#xD;&#xA;14 Marzo-Abril&#xD;&#xA;15 Mayo-Junio&#xD;&#xA;16 Julio-Agosto&#xD;&#xA;17 Septiembre-Octubre&#xD;&#xA;18 Noviembre-Diciembre&#xD;&#xA;19 Enero-Marzo&#xD;&#xA;20 Abril-Junio&#xD;&#xA;21 Julio-Septiembre&#xD;&#xA;22 Octubre-Diciembre&#xD;&#xA;23 Enero-Abril&#xD;&#xA;24 Mayo-Agosto&#xD;&#xA;25 Septiembre-Diciembre&#xD;&#xA;26 Enero-Junio&#xD;&#xA;27 Julio-Diciembre" />                             </xs:appinfo>                           </xs:annotation>                           <xs:simpleType>                             <xs:restriction base="xs:int">                               <xs:maxInclusive value="255" />                             </xs:restriction>                           </xs:simpleType>                         </xs:element>                         <xs:element minOccurs="1" maxOccurs="1" name="MotivoId">                           <xs:annotation>                             <xs:appinfo>                               <b:fieldInfo notes="Motivo del concepto, posibles valores. F, J, I" />                             </xs:appinfo>                           </xs:annotation>                           <xs:simpleType>                             <xs:restriction base="xs:string">                               <xs:minLength value="1" />                             </xs:restriction>                           </xs:simpleType>                         </xs:element>                         <xs:element minOccurs="0" maxOccurs="1" name="FechaCausacion">                           <xs:simpleType>                             <xs:restriction base="xs:string">                               <xs:pattern value="(0[1-9]|[12][0-9]|3[01])[\/](0[1-9]|1[012])[\/](19|20)\d\d|" />                             </xs:restriction>                           </xs:simpleType>                         </xs:element>                         <xs:element minOccurs="1" maxOccurs="1" name="CreditoSir" type="xs:int" />                         <xs:element minOccurs="0" maxOccurs="1" name="CredicoARCA" type="xs:string" />                         <xs:element minOccurs="1" maxOccurs="1" name="ImporteHistorico">                           <xs:simpleType>                             <xs:restriction base="xs:decimal">                               <xs:pattern value="^\d{1,14}$" />                             </xs:restriction>                           </xs:simpleType>                         </xs:element>                         <xs:element minOccurs="1" maxOccurs="1" name="ImporteParteActualizada">                           <xs:simpleType>                             <xs:restriction base="xs:decimal">                               <xs:pattern value="^\d{1,14}$" />                             </xs:restriction>                           </xs:simpleType>                         </xs:element>                         <xs:element minOccurs="1" maxOccurs="1" name="ImportePagar">                           <xs:simpleType>                             <xs:restriction base="xs:decimal">                               <xs:pattern value="^\d{1,14}$" />                             </xs:restriction>                           </xs:simpleType>                         </xs:element>                         <xs:element minOccurs="0" maxOccurs="1" name="PagosAnteriores" type="xs:decimal" />                         <xs:element minOccurs="0" maxOccurs="1" name="FechaNotificacion">                           <xs:simpleType>                             <xs:restriction base="xs:string">                               <xs:pattern value="(0[1-9]|[12][0-9]|3[01])[/.](0[1-9]|1[012])[/.](19|20)\d\d" />                             </xs:restriction>                           </xs:simpleType>                         </xs:element>                         <xs:element minOccurs="1" maxOccurs="1" name="Liquidar" type="xs:boolean" />                         <xs:element minOccurs="0" maxOccurs="1" name="Descuentos">                           <xs:complexType>                             <xs:sequence>                               <xs:element minOccurs="1" maxOccurs="unbounded" name="Descuento">                                 <xs:complexType>                                   <xs:sequence>                                     <xs:element minOccurs="1" maxOccurs="1" name="IdDescuento">                                       <xs:simpleType>                                         <xs:restriction base="xs:string">                                           <xs:pattern value="^\d{6}-\d{4}" />                                         </xs:restriction>                                       </xs:simpleType>                                     </xs:element>                                     <xs:element minOccurs="1" maxOccurs="1" name="ImporteDescuento">                                       <xs:simpleType>                                         <xs:restriction base="xs:decimal">                                           <xs:pattern value="^\d{1,14}$" />                                         </xs:restriction>                                       </xs:simpleType>                                     </xs:element>                                   </xs:sequence>                                 </xs:complexType>                               </xs:element>                             </xs:sequence>                           </xs:complexType>                         </xs:element>                         <xs:element minOccurs="0" maxOccurs="1" name="Hijos">                           <xs:complexType>                             <xs:sequence>                               <xs:element minOccurs="0" maxOccurs="unbounded" name="Hijo">                                 <xs:complexType>                                   <xs:sequence>                                     <xs:element minOccurs="1" maxOccurs="1" name="Clave">                                       <xs:annotation>                                         <xs:appinfo>                                           <b:fieldInfo notes="Clave del concepto ley" />                                         </xs:appinfo>                                       </xs:annotation>                                       <xs:simpleType>                                         <xs:restriction base="xs:string">                                           <xs:maxLength value="6" />                                         </xs:restriction>                                       </xs:simpleType>                                     </xs:element>                                     <xs:element minOccurs="1" maxOccurs="1" name="MotivoId">                                       <xs:annotation>                                         <xs:appinfo>                                           <b:fieldInfo notes="Motivo del concepto, posibles valores. F, J, I" />                                         </xs:appinfo>                                       </xs:annotation>                                       <xs:simpleType>                                         <xs:restriction base="xs:string">                                           <xs:minLength value="1" />                                         </xs:restriction>                                       </xs:simpleType>                                     </xs:element>                                     <xs:element minOccurs="0" maxOccurs="1" name="FechaCausacion">                                       <xs:simpleType>                                         <xs:restriction base="xs:string">                                           <xs:pattern value="(0[1-9]|[12][0-9]|3[01])[\/](0[1-9]|1[012])[\/](19|20)\d\d|" />                                         </xs:restriction>                                       </xs:simpleType>                                     </xs:element>                                     <xs:element minOccurs="1" maxOccurs="1" name="CreditoSir" type="xs:int" />                                     <xs:element minOccurs="1" maxOccurs="1" name="ImporteHistorico">                                       <xs:simpleType>                                         <xs:restriction base="xs:decimal">                                           <xs:pattern value="^\d{1,14}$" />                                         </xs:restriction>                                       </xs:simpleType>                                     </xs:element>                                     <xs:element minOccurs="1" maxOccurs="1" name="ImporteParteActualizada">                                       <xs:simpleType>                                         <xs:restriction base="xs:decimal">                                           <xs:pattern value="^\d{1,14}$" />                                         </xs:restriction>                                       </xs:simpleType>                                     </xs:element>                                     <xs:element minOccurs="1" maxOccurs="1" name="ImportePagar">                                       <xs:simpleType>                                         <xs:restriction base="xs:decimal">                                           <xs:pattern value="^\d{1,14}$" />                                         </xs:restriction>                                       </xs:simpleType>                                     </xs:element>                                     <xs:element minOccurs="0" maxOccurs="1" name="PagosAnteriores" type="xs:decimal" />                                     <xs:element minOccurs="0" maxOccurs="1" name="FechaNotificacion">                                       <xs:simpleType>                                         <xs:restriction base="xs:string">                                           <xs:pattern value="(0[1-9]|[12][0-9]|3[01])[/.](0[1-9]|1[012])[/.](19|20)\d\d" />                                         </xs:restriction>                                       </xs:simpleType>                                     </xs:element>                                     <xs:element minOccurs="0" maxOccurs="1" name="Ejercicio">                                       <xs:simpleType>                                         <xs:restriction base="xs:int">                                           <xs:minInclusive value="1900" />                                           <xs:maxInclusive value="2100" />                                         </xs:restriction>                                       </xs:simpleType>                                     </xs:element>                                     <xs:element minOccurs="1" maxOccurs="1" name="PeriodicidadId">                                       <xs:annotation>                                         <xs:appinfo>                                           <b:fieldInfo notes="1-Mensual. 2-Bimestral. 3-Cuatrimestral. 4-Trimestral. 5-Semestral. 9. Sin Periodo" />                                         </xs:appinfo>                                       </xs:annotation>                                       <xs:simpleType>                                         <xs:restriction base="xs:int">                                           <xs:maxInclusive value="255" />                                         </xs:restriction>                                       </xs:simpleType>                                     </xs:element>                                     <xs:element minOccurs="1" maxOccurs="1" name="PeriodoId">                                       <xs:annotation>                                         <xs:appinfo>                                           <b:fieldInfo notes="1 Enero&#xD;&#xA;2 Febrero&#xD;&#xA;3 Marzo&#xD;&#xA;4 Abril&#xD;&#xA;5 Mayo&#xD;&#xA;6 Junio&#xD;&#xA;7 Julio&#xD;&#xA;8 Agosto&#xD;&#xA;9 Septiembre&#xD;&#xA;10 Octubre&#xD;&#xA;11 Noviembre&#xD;&#xA;12 Diciembre&#xD;&#xA;13 Enero-Febrero&#xD;&#xA;14 Marzo-Abril&#xD;&#xA;15 Mayo-Junio&#xD;&#xA;16 Julio-Agosto&#xD;&#xA;17 Septiembre-Octubre&#xD;&#xA;18 Noviembre-Diciembre&#xD;&#xA;19 Enero-Marzo&#xD;&#xA;20 Abril-Junio&#xD;&#xA;21 Julio-Septiembre&#xD;&#xA;22 Octubre-Diciembre&#xD;&#xA;23 Enero-Abril&#xD;&#xA;24 Mayo-Agosto&#xD;&#xA;25 Septiembre-Diciembre&#xD;&#xA;26 Enero-Junio&#xD;&#xA;27 Julio-Diciembre" />                                         </xs:appinfo>                                       </xs:annotation>                                       <xs:simpleType>                                         <xs:restriction base="xs:int">                                           <xs:maxInclusive value="255" />                                         </xs:restriction>                                       </xs:simpleType>                                     </xs:element>                                     <xs:element minOccurs="1" maxOccurs="1" name="Liquidar" type="xs:boolean" />                                     <xs:element minOccurs="0" maxOccurs="1" name="Descuentos">                                       <xs:complexType>                                         <xs:sequence>                                           <xs:element minOccurs="1" maxOccurs="unbounded" name="Descuento">                                             <xs:complexType>                                               <xs:sequence>                                                 <xs:element minOccurs="1" maxOccurs="1" name="IdDescuento">                                                   <xs:simpleType>                                                     <xs:restriction base="xs:string">                                                       <xs:pattern value="^\d{6}-\d{4}" />                                                     </xs:restriction>                                                   </xs:simpleType>                                                 </xs:element>                                                 <xs:element minOccurs="1" maxOccurs="1" name="ImporteDescuento">                                                   <xs:simpleType>                                                     <xs:restriction base="xs:decimal">                                                       <xs:pattern value="^\d{1,14}$" />                                                     </xs:restriction>                                                   </xs:simpleType>                                                 </xs:element>                                               </xs:sequence>                                             </xs:complexType>                                           </xs:element>                                         </xs:sequence>                                       </xs:complexType>                                     </xs:element>                                   </xs:sequence>                                 </xs:complexType>                               </xs:element>                             </xs:sequence>                           </xs:complexType>                         </xs:element>                       </xs:sequence>                     </xs:complexType>                   </xs:element>                 </xs:sequence>               </xs:complexType>             </xs:element>           </xs:sequence>         </xs:complexType>       </xs:element>     </xs:sequence>   </xs:complexType> </xs:element>             </xs:sequence>           </xs:complexType>         </xs:element>       </xs:sequence>     </xs:complexType>   </xs:element> </xs:schema>'
  )
   SELECT @idEsquema=MAX(IdEsquema) FROM dbo.CatEsquemas
   
	insert into TblAdmonEsquemas values(@idEsquema,5,10,1)
	
	select @idEsquema=IdEsquema from dbo.CatEsquemas where descripcion='Esquema DyP Pagos en una sola exhibición.'
	
	insert into TblAdmonEsquemas values(@idEsquema,5,10,2)

end

--TblAdmonReglas
if  (select count(*) from TblAdmonReglas where idaplicacion=5 and idtipodocumento=10 and EsSIAT=0)=0
 begin
 
 --Agregando nueva regla de validacion para MAT, de momento no es necesario ya que se van a solicitar todos los campos desde el contrato
-- INSERT INTO dbo.CatReglas
--       (Descripcion,Regla,EsValidacion)
--VALUES ('Transformacion entrada Pago Una Sola Exhibicion a Esquema Canonico para MAT',
--        '<?xml version="1.0" encoding="utf-8"?> <xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" xmlns:userCSharp="http://schemas.microsoft.com/BizTalk/2003/userCSharp" exclude-result-prefixes="msxsl var userCSharp" version="1.0">   <xsl:output omit-xml-declaration="yes" method="xml" version="1.0" />   <xsl:template match="/">     <xsl:apply-templates select="/SolicitudCreditosFiscales" />   </xsl:template>   <xsl:template match="/SolicitudCreditosFiscales">     <xsl:variable name="var:v1" select="userCSharp:StringConcat(&quot;0&quot;)" />     <CreditosFiscalesSIAT>       <DatosGenerales>         <xsl:choose>            <xsl:when test="string-length(Mensaje/DatosGenerales/IdPersona/text()) &gt; 0">            <IdPersona><xsl:value-of select="Mensaje/DatosGenerales/IdPersona/text()" />            </IdPersona>            <IdTipoLinea><xsl:value-of select="Mensaje/DatosGenerales/LineaCaptura/Tipo/text()" />            </IdTipoLinea>            <EsSIAT><xsl:text>1</xsl:text>             </EsSIAT>            </xsl:when>           <xsl:otherwise>             <EsSIAT>  <xsl:text>0</xsl:text>             </EsSIAT>          </xsl:otherwise>       </xsl:choose>         <xsl:if test="string-length(Mensaje/DatosGenerales/Convenio/IdConvenio/text()) &gt; 0">           <Convenio>             <IdConvenio> <xsl:value-of select="Mensaje/DatosGenerales/Convenio/IdConvenio/text()" />             </IdConvenio>             <TipoConvenio> <xsl:value-of select="Mensaje/DatosGenerales/Convenio/TipoConvenio/text()" />             </TipoConvenio>             <NumeroConvenio> <xsl:value-of select="Mensaje/DatosGenerales/Convenio/NumeroConvenio/text()" />             </NumeroConvenio>             <ImporteSaldoInsolutoInicial> <xsl:value-of select="Mensaje/DatosGenerales/Convenio/ImporteSaldoInsolutoInicial/text()" />             </ImporteSaldoInsolutoInicial>             <ImporteSaldoInsoluto> <xsl:value-of select="Mensaje/DatosGenerales/Convenio/ImporteSaldoInsoluto/text()" />             </ImporteSaldoInsoluto>           </Convenio>         </xsl:if>       </DatosGenerales>       <Agrupadores>         <xsl:for-each select="Mensaje/ResolucionesDeterminante/ResolucionDeterminante">           <Agrupador>             <IdResolucion> <xsl:value-of select="IdResolucion/text()" />             </IdResolucion>             <ConceptosOriginal> <xsl:for-each select="Conceptos/Concepto">   <Conceptos>     <IdConcepto>       <xsl:value-of select="IdConcepto/text()" />     </IdConcepto>   </Conceptos> </xsl:for-each>             </ConceptosOriginal>           </Agrupador>         </xsl:for-each>       </Agrupadores>     </CreditosFiscalesSIAT>   </xsl:template>   <msxsl:script language="C#" implements-prefix="userCSharp"> public string StringConcat(string param0) {    return param0; }   public bool LogicalEq(string val1, string val2) {  bool ret = false;  double d1 = 0;  double d2 = 0;  if (IsNumeric(val1, ref d1) &amp;&amp; IsNumeric(val2, ref d2))  {   ret = d1 == d2;  }  else  {   ret = String.Compare(val1, val2, StringComparison.Ordinal) == 0;  }  return ret; }   public bool IsNumeric(string val) {  if (val == null)  {   return false;  }  double d = 0;  return Double.TryParse(val, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out d); }  public bool IsNumeric(string val, ref double d) {  if (val == null)  {   return false;  }  return Double.TryParse(val, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out d); }   </msxsl:script> </xsl:stylesheet>',
--        1)

insert into TblAdmonReglas  select idregla,5,IdTipoDocumento,Secuencia,AntesDeInsercion,EsSIAT 
from TblAdmonReglas where idaplicacion=2 and idtipodocumento=10 and EsSIAT=0

insert into TblAdmonReglas  select idregla,5,IdTipoDocumento,Secuencia,AntesDeInsercion,EsSIAT 
from TblAdmonReglas where IdRegla in(
Select IdRegla from  CatReglas where descripcion= 'Transformacion entrada Pago Una Sola Exhibicion a Esquema Canonico para SIAT'
) and idaplicacion=2
end
GO
--Agregando mapeo de ADR's con MAT.
GO
IF (select COUNT(*) from sys.tables t
inner join sys.columns c
on t.object_id=c.object_id
where t.name ='CatALR')=13
begin
ALTER TABLE [dbo].[CatALR] ADD	[IdALRMAT] INT NULL;
select top(1)'Se crea columna' from  [CatALR];
end
UPDATE CatALR SET IdALRMAT=	1946	 WHERE IdALR=	61	;
UPDATE CatALR SET IdALRMAT=	1947	 WHERE IdALR=	42	;
UPDATE CatALR SET IdALRMAT=	1948	 WHERE IdALR=	41	;
UPDATE CatALR SET IdALRMAT=	1949	 WHERE IdALR=	47	;
UPDATE CatALR SET IdALRMAT=	1950	 WHERE IdALR=	43	;
UPDATE CatALR SET IdALRMAT=	1951	 WHERE IdALR=	40	;
UPDATE CatALR SET IdALRMAT=	1952	 WHERE IdALR=	72	;
UPDATE CatALR SET IdALRMAT=	1953	 WHERE IdALR=	76	;
UPDATE CatALR SET IdALRMAT=	1954	 WHERE IdALR=	78	;
UPDATE CatALR SET IdALRMAT=	1955	 WHERE IdALR=	54	;
UPDATE CatALR SET IdALRMAT=	1956	 WHERE IdALR=	53	;
UPDATE CatALR SET IdALRMAT=	1957	 WHERE IdALR=	52	;
UPDATE CatALR SET IdALRMAT=	1958	 WHERE IdALR=	51	;
UPDATE CatALR SET IdALRMAT=	1959	 WHERE IdALR=	57	;
UPDATE CatALR SET IdALRMAT=	1960	 WHERE IdALR=	62	;
UPDATE CatALR SET IdALRMAT=	1961	 WHERE IdALR=	55	;
UPDATE CatALR SET IdALRMAT=	1962	 WHERE IdALR=	7	;
UPDATE CatALR SET IdALRMAT=	1963	 WHERE IdALR=	2	;
UPDATE CatALR SET IdALRMAT=	1964	 WHERE IdALR=	1	;
UPDATE CatALR SET IdALRMAT=	1965	 WHERE IdALR=	27	;
UPDATE CatALR SET IdALRMAT=	1966	 WHERE IdALR=	30	;
UPDATE CatALR SET IdALRMAT=	1967	 WHERE IdALR=	5	;
UPDATE CatALR SET IdALRMAT=	1968	 WHERE IdALR=	63	;
UPDATE CatALR SET IdALRMAT=	1969	 WHERE IdALR=	66	;
UPDATE CatALR SET IdALRMAT=	1970	 WHERE IdALR=	67	;
UPDATE CatALR SET IdALRMAT=	1971	 WHERE IdALR=	65	;
UPDATE CatALR SET IdALRMAT=	1972	 WHERE IdALR=	68	;
UPDATE CatALR SET IdALRMAT=	1973	 WHERE IdALR=	16	;
UPDATE CatALR SET IdALRMAT=	1974	 WHERE IdALR=	15	;
UPDATE CatALR SET IdALRMAT=	1975	 WHERE IdALR=	3	;
UPDATE CatALR SET IdALRMAT=	1976	 WHERE IdALR=	8	;
UPDATE CatALR SET IdALRMAT=	1977	 WHERE IdALR=	28	;
UPDATE CatALR SET IdALRMAT=	1978	 WHERE IdALR=	64	;
UPDATE CatALR SET IdALRMAT=	1979	 WHERE IdALR=	36	;
UPDATE CatALR SET IdALRMAT=	1980	 WHERE IdALR=	31	;
UPDATE CatALR SET IdALRMAT=	1981	 WHERE IdALR=	35	;
UPDATE CatALR SET IdALRMAT=	1982	 WHERE IdALR=	71	;
UPDATE CatALR SET IdALRMAT=	1983	 WHERE IdALR=	23	;
UPDATE CatALR SET IdALRMAT=	1984	 WHERE IdALR=	21	;
UPDATE CatALR SET IdALRMAT=	1985	 WHERE IdALR=	4	;
UPDATE CatALR SET IdALRMAT=	1986	 WHERE IdALR=	77	;
UPDATE CatALR SET IdALRMAT=	1987	 WHERE IdALR=	73	;
UPDATE CatALR SET IdALRMAT=	1988	 WHERE IdALR=	6	;
UPDATE CatALR SET IdALRMAT=	1989	 WHERE IdALR=	44	;
UPDATE CatALR SET IdALRMAT=	1990	 WHERE IdALR=	49	;
UPDATE CatALR SET IdALRMAT=	1991	 WHERE IdALR=	48	;
UPDATE CatALR SET IdALRMAT=	1992	 WHERE IdALR=	46	;
UPDATE CatALR SET IdALRMAT=	1993	 WHERE IdALR=	45	;
UPDATE CatALR SET IdALRMAT=	1994	 WHERE IdALR=	50	;
UPDATE CatALR SET IdALRMAT=	1995	 WHERE IdALR=	74	;
UPDATE CatALR SET IdALRMAT=	1996	 WHERE IdALR=	39	;
UPDATE CatALR SET IdALRMAT=	1997	 WHERE IdALR=	38	;
UPDATE CatALR SET IdALRMAT=	1998	 WHERE IdALR=	37	;
UPDATE CatALR SET IdALRMAT=	1999	 WHERE IdALR=	32	;
UPDATE CatALR SET IdALRMAT=	2000	 WHERE IdALR=	33	;
UPDATE CatALR SET IdALRMAT=	2001	 WHERE IdALR=	22	;
UPDATE CatALR SET IdALRMAT=	2002	 WHERE IdALR=	24	;
UPDATE CatALR SET IdALRMAT=	2003	 WHERE IdALR=	25	;
UPDATE CatALR SET IdALRMAT=	2004	 WHERE IdALR=	29	;
UPDATE CatALR SET IdALRMAT=	2005	 WHERE IdALR=	26	;
UPDATE CatALR SET IdALRMAT=	2006	 WHERE IdALR=	34	;
UPDATE CatALR SET IdALRMAT=	2007	 WHERE IdALR=	75	;
UPDATE CatALR SET IdALRMAT=	2008	 WHERE IdALR=	56	;
UPDATE CatALR SET IdALRMAT=	2009	 WHERE IdALR=	11	;
UPDATE CatALR SET IdALRMAT=	2010	 WHERE IdALR=	12	;
UPDATE CatALR SET IdALRMAT=	2011	 WHERE IdALR=	14	;
UPDATE CatALR SET IdALRMAT=	2011	 WHERE IdALR=	13	;
--pConsultaFormatos
IF (SELECT COUNT(*) FROM sys.procedures WHERE name = 'pConsultaFormatos')=1
begin
DROP PROCEDURE [dbo].[pConsultaFormatos]
end 
go
CREATE PROCEDURE [dbo].[pConsultaFormatos]
	-- Add the parameters for the stored procedure here
	@ADR int,
	@RFC varchar(20),
	@Folio varchar(20),
	@LineaDeCaptura varchar(100),
	@No_de_Resolucion varchar(100),
	@rango_de_emision_FechaPago int,
	@fecha_ini varchar(100),
	@fecha_fin varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    
	Declare @abredbc varchar(MAX);
	Declare @incluirFecha bit;
	SET @incluirFecha=1;
set @abredbc=N'SELECT ADRCat.Descripcion ADR,DATgral.[RFC],DATgral.[Nombre],DATgral.[ApellidoPaterno] 
,DATgral.[ApellidoMaterno],
DATgral.[RazonSocial],
DATgral.[FolioDyP],
DATgral.[FechaEmision],
SAA.IdResolucion
		,AAA.ValorAgrupador
		, DATgral.[LineaCaptura],DATgral.ImporteTotalPagar,DATgral.FechaVigencia,DATgral.FormaPago ,DATgral.[FechaPago],CATAPP.Nombre Aplicacion
      
  FROM [MotorTraductor].[dbo].[TblDatosGenerales] AS DATgral
  INNER JOIN [MotorTraductor].[dbo].TblSIATAgrupador AS SAA ON DATgral.IdProcesamiento =SAA.IdProcesamiento
  INNER JOIN [MotorTraductor].[dbo].[TblAgrupador] AS AAA ON AAA.IdProcesamiento=DATgral.IdProcesamiento
  INNER JOIN [MotorTraductor].[dbo].[CatAplicacion] AS CATAPP ON CATAPP.IdAplicacion=DATgral.IdAplicacion 
  INNER JOIN [MotorTraductor].[dbo].[CatALR] AS ADRCat ON ADRCat.IdALR=DATgral.ClaveAlr
  
  WHERE DATgral.[RFC] is NOT null '
  IF(@ADR IS NOT NULL and @ADR<>'')
  BEGIN
	
	set	 @abredbc+=N' AND ADRCat.[IdALRMAT] = '+ CONVERT(CHAR,@ADR);
	END
  
  IF(@RFC IS NOT NULL and @RFC<>'')
  BEGIN
	
	set	 @abredbc+=N' AND DATgral.[RFC] like '+ char(39)+'%'+ + @RFC +'%'+ char(39);
	END
	
	IF(@Folio IS NOT NULL and @Folio<>'')
  BEGIN
	
	set	 @abredbc+=N' AND DATgral.[FolioDyP] like '+ char(39)+'%'+ + @Folio +'%'+ char(39);
	set @incluirFecha=0;
	END
	IF(@LineaDeCaptura IS NOT NULL and @LineaDeCaptura<>'')
  BEGIN
	
	set	 @abredbc+=N' AND DATgral.[LineaCaptura] like '+ char(39)+'%'+ + @LineaDeCaptura +'%'+ char(39);
		set @incluirFecha=0;
	END
	IF(@No_de_Resolucion IS NOT NULL and @No_de_Resolucion<>'')
  BEGIN
	
	set	 @abredbc+=N' AND SAA.[IdResolucion] like '+ char(39)+'%'+ + @No_de_Resolucion +'%'+ char(39);
	END
 IF (@incluirFecha=1)
 BEGIN
   IF(@rango_de_emision_FechaPago=1)
   BEGIN
   set	 @abredbc+=N' AND DATgral.[FechaEmision] BETWEEN '+ char(39) + @fecha_ini + char(39)+' AND '+ char(39)+ @fecha_fin + char(39);
   END
   IF(@rango_de_emision_FechaPago=2)
   BEGIN
   set	 @abredbc+=N' AND DATgral.[FechaPago] BETWEEN '+ char(39) + @fecha_ini + char(39)+' AND '+ char(39)+ @fecha_fin + char(39);
   END
 END
 PRINT (@abredbc)
exec(@abredbc)
END

GO
--TblSIATDatosGenerales
ALTER TABLE TblSIATDatosGenerales ALTER COLUMN IdTipoLinea int ; 
--pInsertaSIATDatosGenerales
IF (SELECT COUNT(*) FROM sys.procedures WHERE name = 'pInsertaSIATDatosGenerales')=1
begin
DROP PROCEDURE [dbo].[pInsertaSIATDatosGenerales]
end 
go
CREATE PROCEDURE [dbo].[pInsertaSIATDatosGenerales](
	@pIdProcesamiento UNIQUEIDENTIFIER,
	@pIdPersona varchar(22),
	@pIdConvenio varchar(22),
	@pTipoConvenio int,
	@pNumeroConvenio varchar(250),
	@pImporteSaldoInsolutoInicial bigint,
	@pImporteSaldoInsoluto bigint,
	@pNumeroParcialidad tinyint,
    @pTotalDeParcialidades tinyint,
    @pMuestraTotalDeParcialidades bit,
    @pSeparador varchar(4), 
    @pDescripcionParcialidad varchar(20),
    @pIdTipoLinea int   
)
AS
BEGIN	
		
	BEGIN TRY	
	
		BEGIN TRANSACTION;
				
		INSERT INTO dbo.TblSIATDatosGenerales(
			IdProcesamiento,
			IdPersona,
            IdConvenio,
            TipoConvenio,
			NumeroConvenio,
			ImporteSaldoInsolutoInicial,
			ImporteSaldoInsoluto,
			TotalDeParcialidades,
			MuestraTotalDeParcialidades,
			Separador,
			DescripcionParcialidad,
			IdTipoLinea
		)
		VALUES
		(
			@pIdProcesamiento,
			@pIdPersona ,
			@pIdConvenio,
			@pTipoConvenio,
			@pNumeroConvenio,
			@pImporteSaldoInsolutoInicial,
			@pImporteSaldoInsoluto,
    		@pTotalDeParcialidades,
    		@pMuestraTotalDeParcialidades,
    		@pSeparador, 
    		@pDescripcionParcialidad,
    		@pIdTipoLinea
		)
		
		UPDATE dbo.TblDatosGenerales
		SET NumeroParcialidad= @pNumeroParcialidad
		WHERE IdProcesamiento=@pIdProcesamiento
				
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH	
	
		ROLLBACK TRANSACTION;
		
		DECLARE 
			@ErrorNumber		INT
			,@ErrorMessage		NVARCHAR(4000)
			,@ErrorSeverity		INT
			,@ErrorState		INT
			,@ErrorProcedure	NVARCHAR(126)
			,@msg				NVARCHAR(2000)

		SELECT  
			@ErrorNumber		= ERROR_NUMBER()
			,@ErrorMessage		= ERROR_MESSAGE()
			,@ErrorSeverity		= ERROR_SEVERITY()
			,@ErrorState		= ERROR_STATE()
			,@ErrorProcedure	= ERROR_PROCEDURE()
	                
		SELECT  
			@msg = 
				'Error No. ' 
				+ CONVERT (NVARCHAR(10), @ErrorNumber)
				+ '. Descripción: ' 
				+ @ErrorMessage + ' Origen del Error: '
				+ @ErrorProcedure 
	        
		RAISERROR ( @msg, @ErrorSeverity, @ErrorState )
		
	END CATCH
END
GO

select count(*) AS MAT from cataplicacion where idaplicacion=5
select count(*) AS ARCA from cataplicacion where idaplicacion=2

select count(*) AS MAT from CatConceptoEquivalencia where idaplicacion=5
select count(*) AS ARCA from CatConceptoEquivalencia where idaplicacion=2 

select count(*) AS MAT from CatConceptosOriginal where idaplicacion=5
select count(*) AS ARCA from CatConceptosOriginal  where idaplicacion=2 

select count(*) AS MAT from CatFormatoImpresionEquivalencia where idaplicacion=5 and idtipodocumento=10
select count(*) AS ARCA from CatFormatoImpresionEquivalencia where idaplicacion=2 and idtipodocumento=10

select count(*) AS MAT from CatOrdenamientoTransaccion where idaplicacion=5 and idtipodocumento=10
select count(*) AS ARCA from CatOrdenamientoTransaccion where idaplicacion=2 and idtipodocumento=10

select count(*) AS MAT from CatPeriodicidadEquivalencia where idaplicacion=5
select  count(*) AS ARCA from CatPeriodicidadEquivalencia where idaplicacion=2

select count(*) AS MAT from CatPeriodoEquivalencia where idaplicacion=5
select count(*) AS ARCA from CatPeriodoEquivalencia where idaplicacion=2

select count(*) AS MAT from CatReglasEquivalencia where idaplicacion=5 and idtipodocumento=10
select count(*) AS ARCA from CatReglasEquivalencia where idaplicacion=2 and idtipodocumento=10

select count(*) AS MAT from CatTransaccion where idaplicacion=5 and idtipodocumento=10
select count(*) AS ARCA from CatTransaccion where idaplicacion=2 and idtipodocumento=10

select count(*) AS MAT  from TblAdmonEsquemas where idaplicacion=5 and idtipodocumento=10
select count(*) AS ARCA from TblAdmonEsquemas where idaplicacion=2 and idtipodocumento=10 

select count(*) AS MAT from TblAdmonReglas where idaplicacion=5  and idtipodocumento=10
select count(*) AS ARCA  from TblAdmonReglas where idaplicacion=2  and idtipodocumento=10