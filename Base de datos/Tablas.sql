
drop database  if exists dbGuarderia
create database dbGuarderia
go
use dbguarderia
go
CREATE TABLE Childs(
IDmatricula int identity(1,1)primary key NOT NULL,
Nombre varchar(150) not null,
FechaRegistro datetime default getdate(),
FechaNacimiento date NULL,
)
go
---Un niño tendra relaciones con ciertas personas
----Una relacion puede ser un tio, puede tener varios tios, un tio o ningin tio.
CREATE TABLE childsRelations(
ID int identity(1,1)primary key NOT NULL,
IDchild int not null ,
TipoRelacion varchar(30) not null default 'Abonado',
)
go
CREATE TABLE Abonados(
ID int identity(1,1)primary key NOT NULL,
IDchildRelation int not null ,
DNI VARCHAR(15) NOT NULL,
Nombre varchar(150) not null,
Direccion varchar(100) null,
Telefono varchar(10) not null,
Banco varchar(20) not null,
CuentaIBAM varchar(35) not null,
)
go
CREATE TABLE Encargados(
ID int identity(1,1)primary key NOT NULL,
IDchildRelation int not null ,
DNI VARCHAR(15) NOT NULL,
Nombre varchar(150) not null,
Direccion varchar(100) null,
Telefono varchar(10) not null,
)
go
CREATE TABLE Alergias(
ID int identity(1,1)primary key NOT NULL,
NombreIngrediente Varchar(50) not null ,
IDchild int not null ,
)
go
CREATE TABLE Ingredientes(
Nombre Varchar(50) primary key NOT NULL,
)
go
CREATE TABLE IngredientesDeplato(
ID int identity(1,1)primary key NOT NULL,
NombrePlato Varchar(50) not null ,
NombreIngrediente Varchar(50) not null ,
)
go
CREATE TABLE Platos(
Nombre Varchar(50) primary key NOT NULL ,
)
go
CREATE TABLE platosDemenu(
ID int identity(1,1)primary key NOT NULL,
NombrePlato Varchar(50) not null ,
IDmenu int not null ,
)
go
CREATE TABLE Menus(
ID int identity(1,1)primary key NOT NULL,
Nombre Varchar(50) not null ,
Precio decimal(10,2) not null ,
)
go

CREATE TABLE Consumos(
ID int identity(1,1)primary key NOT NULL,
IDchild int not null ,
IDmenu int not null ,
FechaConsumo date default getdate(),
SNCANCELADO BIT DEFAULT 0 NOT NULL ,
)
go

CREATE TABLE DetalleConsumos (
ID int identity(1,1)primary key NOT NULL,
IDfactura int not null ,
IDconsumo int not null ,
FechaCreacion  datetime default getDate() not null ,
)

go
drop table if exists Asistencias
go
CREATE TABLE Asistencias(
ID int identity(1,1)primary key NOT NULL,
IDchild int not null ,
FechaRegistro  datetime default getDate() not null ,
MES			VARCHAR(25)  default datename(month,getdate()),
HoraEntrada time  default getdate (),
HoraSalida time default  dateadd(hour,5, getdate ()),   ---Regista el ingreso y le manda 5 horas como minimo si el usuario no le manda nada
Detalles varchar(100) null,
SNCANCELADO BIT DEFAULT 0 NOT NULL ,
)
go
CREATE TABLE DetalleAsistencias(
ID int identity(1,1)primary key NOT NULL,
IDfactura int not null ,
IDasistencia int not null ,
FechaCreacion  datetime default getDate() not null ,
)


CREATE TABLE Mfacturas(
ID int identity(1,1)primary key NOT NULL,
IDabonado int not null ,
MES			VARCHAR(25)  default datename(month,getdate()),
FechaCreacion  datetime default getDate() not null ,
)
go
-----Constraints 
use dbGuarderia
go



ALTER TABLE childsRelations
   ADD CONSTRAINT  childsRelation_FK_TO_childs 
   FOREIGN KEY (IDchild) REFERENCES Childs (IDmatricula)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
;
go
ALTER TABLE Encargados
   ADD CONSTRAINT  Encargados_FK_TO_ChildsRelations 
   FOREIGN KEY (IDchildRelation) REFERENCES childsRelations (ID)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
;
go

ALTER TABLE Abonados
   ADD CONSTRAINT  Abonados_FK_TO_ChildsRelations 
   FOREIGN KEY (IDchildRelation) REFERENCES ChildsRelations (ID)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
;
go

ALTER TABLE Mfacturas
   ADD CONSTRAINT  Mfacturas_FK_TO_Abonados 
   FOREIGN KEY (IDabonado) REFERENCES Abonados (ID)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
;
go


ALTER TABLE DetalleAsistencias
   ADD CONSTRAINT  DetalleAsistencias_FK_TO_Mfacturas 
   FOREIGN KEY (IDfactura) REFERENCES Mfacturas (ID)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
;
go
ALTER TABLE DetalleAsistencias
   ADD CONSTRAINT  DetalleAsistencias_FK_TO_Asistencias 
   FOREIGN KEY (IDasistencia) REFERENCES Asistencias (ID)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
;
go


ALTER TABLE [dbo].[DetalleConsumos]
   ADD CONSTRAINT  DetalleConsumos_FK_TO_Mfacturas 
   FOREIGN KEY (IDfactura) REFERENCES Mfacturas (ID)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
;
go

ALTER TABLE [dbo].[DetalleConsumos]
   ADD CONSTRAINT  DetalleConsumos_FK_TO_Consumos 
   FOREIGN KEY (IDconsumo) REFERENCES Consumos (ID)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
;
go
ALTER TABLE Asistencias
   ADD CONSTRAINT  Asistenciass_FK_TO_Childs 
   FOREIGN KEY (IDchild) REFERENCES Childs (IDmatricula)
      ON DELETE no action 
      ON UPDATE  no action 
;
go


ALTER TABLE Consumos
   ADD CONSTRAINT  Consumos_FK_TO_Childs 
   FOREIGN KEY (IDchild) REFERENCES Childs (IDmatricula)
    ON DELETE no action 
      ON UPDATE  no action 
;
go

ALTER TABLE Consumos
   ADD CONSTRAINT  Consumos_FK_TO_Menus 
   FOREIGN KEY (IDmenu) REFERENCES Menus (ID)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
;
go


ALTER TABLE platosDemenu
   ADD CONSTRAINT  pLatosDemenu_FK_TO_Menus 
   FOREIGN KEY (IDmenu) REFERENCES Menus (ID)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
;
go
ALTER TABLE platosDemenu
   ADD CONSTRAINT  platosDemenu_FK_TO_platos
   FOREIGN KEY (NombrePlato) REFERENCES Platos (Nombre)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
;
go

ALTER TABLE ingredientesDeplato
   ADD CONSTRAINT  ingredientesDeplato_FK_TO_platos
   FOREIGN KEY (NombrePlato) REFERENCES Platos (Nombre)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
;
go

ALTER TABLE ingredientesDeplato
   ADD CONSTRAINT  ingredientesDeplato_FK_TO_Ingredientes
   FOREIGN KEY (NombreIngrediente) REFERENCES Ingredientes (Nombre)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
;
go

ALTER TABLE Alergias
   ADD CONSTRAINT  Alergias_FK_TO_Ingredientes
   FOREIGN KEY (NombreIngrediente) REFERENCES Ingredientes (Nombre)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
;
go


ALTER TABLE Alergias
   ADD CONSTRAINT  Alergias_FK_TO_Childs
   FOREIGN KEY (IDchild) REFERENCES Childs (IDmatricula)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION
;
go


