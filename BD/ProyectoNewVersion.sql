use master 
go


if exists(Select * FROM SysDataBases WHERE name='ProyectoNewVersion')
BEGIN
DROP DATABASE ProyectoNewVersion
END
go

------------------------------------------------------------

create database ProyectoNewVersion
on
(
name = Proyecto,
filename ='C:\SQL2019\ProyectoNewVersion.mdf'
)
go

------------------------------------------------------------

use ProyectoNewVersion
go

create table Entidades
(
NEntidad Varchar(25) not null primary key ,
Direccion varchar(25) not null
)
go

create table Telefonos--es compusta por am campos
(
NEntidad Varchar(25)not null primary key,
Telefonos varchar(9) not null
foreign key (NEntidad)references Entidades
)
go

create table Usuarios
(
CIEmpleado int primary key not null,
NombreEmpleado varchar(25) not null,
Pass varchar(10)
)
go

create table Tramites
(
CodigoT int not null ,
TipoTramite varchar(25)not null, 
DescripcionT varchar(100) not null,
NEntidad varchar(25)not null foreign key references Entidades(NEntidad) 
primary key (CodigoT, NEntidad)
)
go

create table Solicitudes
(
IDSolicitud int identity (1,1) primary key,
Fecha  date not null,
Hora time not null ,
Estado varchar(25) check (Estado in ('Alta', 'Ejecutada', 'Anulada')),
NombreSolicitante varchar(25) not null,
CodigoT int not null,
NEntidad varchar(25)not null ,
CIEmpleado int foreign key references Usuarios(CIEmpleado),
foreign key (CodigoT, NEntidad) references Tramites (CodigoT, NEntidad)
)
go

------------------------------------------------------------

insert into Entidades(NEntidad, Direccion ) values('OSE', 'Colonia 4455' )
insert into Entidades(NEntidad, Direccion ) values('ANTEL', 'Uruguay 1515' )
insert into Entidades(NEntidad, Direccion ) values('UTE', 'Mendoza 4100' )
go

insert into Telefonos(NEntidad, Telefonos ) values('OSE', '111222333')
insert into Telefonos(NEntidad,Telefonos) values('ANTEL', '333444666')
insert into Telefonos(NEntidad,Telefonos) values('UTE', '555888333')
go

insert into Usuarios(CIEmpleado,NombreEmpleado,Pass) values(11111111, 'Mar','usu1')
insert into Usuarios(CIEmpleado,NombreEmpleado,Pass) values(22222222, 'Sol','usu2')
go

insert into Tramites(CodigoT,TipoTramite, DescripcionT,NEntidad) values(1, 'Otro','publica','OSE')
insert into Tramites(CodigoT,TipoTramite, DescripcionT,NEntidad) values(2, 'Otro','publica','ANTEL')
insert into Tramites(CodigoT,TipoTramite, DescripcionT,NEntidad) values(3, 'Otro','publica','UTE')
go

insert into Solicitudes (Fecha,Hora,Estado,NombreSolicitante, CodigoT, NEntidad, CIEmpleado)
values('20200303','1325','Alta','ss', 1,'OSE', 11111111)
insert into Solicitudes (Fecha,Hora,Estado,NombreSolicitante, CodigoT, NEntidad, CIEmpleado)
values('20200802','2450','Ejecutada','aa', 2,'ANTEL', 11111111)
insert into Solicitudes (Fecha,Hora,Estado,NombreSolicitante, CodigoT, NEntidad, CIEmpleado)
values('20200409','1042','Anulada','rr', 3,'UTE', 22222222)
go 

------------------------------------------------------------

create procedure EliminarEntidades
@NEntidad varchar(25),
@telefonos varchar(9)
as
begin
if not exists (select * from Entidades where NEntidad=@NEntidad)
return 0;

if exists (select NEntidad from Solicitudes where NEntidad=@NEntidad)
return 1;

declare @error int

begin tran

delete Entidades where NEntidad = @NEntidad
set @error=@@ERROR

delete Telefonos where Telefonos=@telefonos
set @error=@@ERROR+@error;

IF(@Error=0)
	BEGIN
		COMMIT TRAN;
		RETURN 1;
	END
	ELSE
	BEGIN
		ROLLBACK TRAN;
		RETURN -2;
	END	
end
go


CREATE PROCEDURE ModificarEntidades  
@NEntidades varchar(25), 
@direccion varchar(25)
AS
Begin
	if Not EXISTS(Select * From Entidades Where NEntidad = @NEntidades)
		return -1
		
	--si llego aca puedo modificar
	UPDATE Entidades 
	SET Direccion=@direccion
	where @NEntidades=NEntidad
	IF(@@Error=0)
		RETURN 1
	ELSE
		RETURN -2
End
go

CREATE PROCEDURE BuscarEntidades 
@NEntidades varchar(25) 
AS
Begin
	Select * From Entidades Where NEntidad = @NEntidades
End
go

CREATE PROCEDURE AgregarEntidades
@NEntidades varchar(25),
@Direccion varchar(25)
AS
Begin
	if EXISTS (Select * From Entidades Where NEntidad = @NEntidades)
		return -1
		
	--si llego aca puedo agregar
	INSERT Entidades(NEntidad, Direccion) VALUES(@NEntidades, @Direccion) 
	
	IF(@@Error=0)
		RETURN 1
	else
		RETURN -2
End
go

create procedure ListarEntidad
as
begin 
select * from Entidades
end
go

------------------------------------------------------------

create procedure EliminarTramites
@NEntidades varchar(25),
@CodigoT int,
@IDSolicitud int
as

begin
	if not exists (select * from Tramites where  @CodigoT=CodigoT)
return 0;

declare @error int 
begin tran

 delete Solicitudes where NEntidad = @NEntidades and CodigoT=@CodigoT
 set @error= @@ERROR;
  
 delete Tramites where NEntidad = @NEntidades and CodigoT=@CodigoT
 set @error=@@error;
 
 IF(@@Error=0)
 BEGIN
		COMMIT TRAN
		RETURN 1
	END
	ELSE
	BEGIN
		ROLLBACK TRAN
		RETURN -2
	END	
end
go

CREATE PROCEDURE ModificarTramites
@CodigoT int,
@NEntidades varchar(25),
@DescripcionT varchar(100),
@tipoTramite varchar(25)
AS
Begin
if not exists (select * from Tramites where CodigoT=@CodigoT and @NEntidades= NEntidad)
return -1


--si llego aca puedo modificar
declare @error int

UPDATE Tramites SET NEntidad=@tipoTramite, DescripcionT=@DescripcionT
where CodigoT = @CodigoT and NEntidad=@NEntidades

IF(@@Error=0)
RETURN 1
ELSE
RETURN -2
End
go

CREATE PROCEDURE BuscarTramites 
@CodigoT int,
@NEntidad varchar
AS
Begin
	Select * From Tramites Where CodigoT = @CodigoT and NEntidad=@NEntidad
End
go

CREATE PROCEDURE AgregarTramites
@CodigoT int,
@NEntidades varchar(25), 
@TipoT varchar(25),
@DescripcionT varchar(max)
AS
Begin
	if (EXISTS(Select * From Tramites Where CodigoT = @CodigoT))
		return -1
		 
		
	--si llego aca puedo agregar
	INSERT Tramites(CodigoT, TipoTramite, DescripcionT, NEntidad) VALUES(@CodigoT, @TipoT, @DescripcionT, @NEntidades) 
	
	IF(@@Error=0)
		RETURN 1
	else
		RETURN -2
End
go

create procedure ListarTramites
as
begin
	select CodigoT,TipoTramite, DescripcionT, NEntidad
	from Tramites
end
go

------------------------------------------------------------

create procedure Logueo
@CI int,
@Pasword varchar(10)
AS
Begin
	Select * 
	From Usuarios u 
	Where u.CIEmpleado = @CI and u.Pass = @Pasword
End
go

------------------------------------------------------------

create procedure UsuarioEmpleadoEliminar
@CIEmpleado int
as
begin 
if  not exists (select * from Usuarios where CIEmpleado= @CIEmpleado)
return -1

if  exists (select * from Solicitudes where CIEmpleado=@CIEmpleado)--se puede poner asi? se que son dos variales distintas y no funciona asi, pero....
return -2

declare @error int 

delete Usuarios where CIEmpleado=@CIEmpleado
set @error=@@error;
 
 IF(@@Error=0)
 BEGIN

	RETURN 1
END
ELSE
BEGIN
	
	RETURN -3
end

end
go

------------------------------------------------------------

create procedure UsuarioEmpleadoModificar
@NombreEmpleado varchar(25),
@Pasword varchar(10),
@CI int
as
begin

	if not exists (select * from Usuarios where CIEmpleado= @CI)
	return -2

	update Usuarios
	set Pass=@Pasword , NombreEmpleado=@NombreEmpleado
	where CIEmpleado=@CI

	IF(@@Error=0)
		RETURN 1
	ELSE
		RETURN -3
end
go

------------------------------------------------------------


create procedure UsuarioEmpleadoAgregar
@NombreEmpleado varchar(25),
@Pasword varchar(max),
@CI int
as
begin
	if exists (select * from Usuarios where CIEmpleado=@CI)
	return -1


	insert Usuarios (NombreEmpleado, Pass, CIEmpleado) values(@NombreEmpleado, @Pasword, @CI)

	IF(@@Error=0)
		RETURN 1
	else
		RETURN -3
end
go

create procedure BuscarUsu
@Ci int
as
begin
	select CIEmpleado, NombreEmpleado, Pass
	from Usuarios
	where CIEmpleado=@Ci
end
go

------------------------------------------------------------


create procedure RegistrarunaSolicitudAlta
@fecha date,
@hora time,
@estado varchar(25),
@nombreS varchar(25),
@ci int,
@CodigoT int,
@Nentidad varchar(25)
as
begin
	if not exists (select * from Usuarios where CIEmpleado= @ci)--verifico si esta el empleado
	return 0;

	if not exists (select * from Tramites where CodigoT=@codigoT and NEntidad=@NEntidad)--verifico que exista el teamite
	return 1;

	declare @error int
	
	insert Solicitudes(Fecha, Hora, Estado, NombreSolicitante, CodigoT, NEntidad, CIEmpleado)--insert de la solicitud
	values(@fecha, @hora, @estado, @nombreS, @codigoT, @NEntidad, @ci)

	IF(@Error=0)
	BEGIN
		RETURN -2;
	END
	ELSE
	BEGIN
		RETURN -1;
	END	

end
go

create procedure CambioEstadoSolicitud
@idSolicitud varchar(25),
@estado varchar
as
begin
	if (NOT EXISTS (SELECT IDSolicitud FROM Solicitudes WHERE IDSolicitud=@idSolicitud))
		RETURN 0;

	DECLARE @Error int

	update Solicitudes
	set Estado=@estado
	where IDSolicitud=@idSolicitud 

	IF(@Error=0)
	BEGIN
		RETURN ;
	END
	ELSE
	BEGIN
		RETURN -1;
	end
end
go

create procedure ListadoEstadoSolicitudes
as
begin
	select * from Solicitudes

end
go

create procedure ListadoSolicitudesporFecha @fecha date as
select * 
from Solicitudes where Fecha=@fecha order by Fecha
go

create procedure BuscarSolicitud
@IDsolicitud int
as
begin
	select *
	from Solicitudes
	where IDSolicitud=@IDsolicitud
end
go