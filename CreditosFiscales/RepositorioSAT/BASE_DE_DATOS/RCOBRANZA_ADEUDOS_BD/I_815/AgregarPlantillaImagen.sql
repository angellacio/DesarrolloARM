USE Templates
GO

IF (SELECT COUNT(Descripcion) FROM dbo.CatDocumento where Descripcion='Imagen')=0

BEGIN
INSERT INTO dbo.CatDocumento 
VALUES ((SELECT MAX(IdDocumento)+1 FROM dbo.CatDocumento),'Imagen','Configuracion','<xs:schema xmlns:xs="http://www.w3.org/2001/XMLSchema" attributeFormDefault="unqualified" elementFormDefault="qualified">   <xs:element name="Documentos">     <xs:complexType>       <xs:sequence>         <xs:element maxOccurs="unbounded" name="LC">           <xs:complexType mixed="true">             <xs:sequence> <xs:element name="CodigoBarrasDetalle">   <xs:complexType>     <xs:sequence>       <xs:element name="CodigoBarras" type="NonEmptyString" />       <xs:element name="ImagenCB" type="xs:string" />       <xs:element name="ImagenQR" type="xs:string" />     </xs:sequence>     <xs:attribute name="template" type="xs:string" use="required" />     <xs:attribute name="version" type="xs:unsignedInt" use="required" />   </xs:complexType> </xs:element> <xs:element name="FechaVigencia" type="NonEmptyString" /> <xs:element name="Leyenda" type="xs:string" />             </xs:sequence>             <xs:attribute name="template" type="xs:string" use="required" />             <xs:attribute name="version" type="xs:unsignedInt" use="required" />           </xs:complexType>         </xs:element>       </xs:sequence>       <xs:attribute name="template" type="xs:string" use="required" />     </xs:complexType>   </xs:element>   <xs:simpleType name="NonEmptyString">     <xs:restriction base="xs:string">       <xs:minLength value="1" />     </xs:restriction>   </xs:simpleType> </xs:schema>')
END 

SELECT [IdDocumento]
      ,[Descripcion]
      ,[Configuracion]
      ,[XsdValidacion]
  FROM [Templates].[dbo].[CatDocumento]