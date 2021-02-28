USE [dbGuarderia]
GO
/****** Object:  StoredProcedure [dbo].[stp_CPABONADOS_ACTUALIZAR]    Script Date: 15/02/2021 11:11:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create or alter  procedure [dbo].[stp_CPEncargados_CRUD] (

@aID int =-1,
@aIDchildRelation int =-1,
@aDNI varchar(15)='' ,
@aNombre varchar(150)='',
@aDireccion varchar (150)='',
@aTelefono varchar (10)='',
@aOperacion varchar(1) = 'n',
@aMensajeError varchar(max) = 'no definido' output  ,
@anumErr int= -1 output  
)
as 
BEGIN

		if(@aOperacion = 'n')
			BEGIN
				return 0;
			END
				----Insertar una Relación
		IF (@aOperacion='i')
			BEGIN
			if(	@aIDchildRelation=-1)
					BEGIN
						set @aMensajeError = 'El Encargado no tiene relación  con el niño.'
						set @anumErr = 0;
						return 0;
					END
				
				if(	@aDNI ='')
					BEGIN
						set @aMensajeError = ' No puede ir vacio.'
						set @anumErr = 0;
						return 0;
					END

				if(	@aNombre ='')
					BEGIN
						set @aMensajeError = 'El Nombre del Encargado no puede ir vacio.'
						set @anumErr = 0;
						return 0;
					END

				if(	@aDireccion ='')
					BEGIN
						set @aMensajeError = 'La Dirección del Encargado no puede ir vacio.'
						set @anumErr = 0;
						return 0;
					END

				if(	@aTelefono ='')
					BEGIN
						set @aMensajeError = 'El Télefono del Encargado no puede ir vacio.'
						set @anumErr = 0;
						return 0;
					END

					INSERT INTO [dbo].[Encargados]
						 ([IDchildRelation],
						 [DNI]
						 ,[Nombre],
						 [Direccion]
					    ,[Telefono])
   
				 VALUES
					   (@aIDchildRelation,
					    @aDNI,
						@aNombre,
						@aDireccion,
					   @aTelefono)
				
					set @aMensajeError = 'El Encargado se ha agregado con éxito.'
						set @anumErr = 1;
					return 1
			END

	
		----Borrar un Relación
		IF (@aOperacion='d')
		BEGIN
		--Hay que eliminar por el ID, EL ID es el Primary key
			if(	@aID =-1)
					BEGIN
						set @aMensajeError = 'El ID del Encargado es nulo, 0 registros afectados.'
						set @anumErr = 0;
						return 0;
					END
			
				DELETE FROM [dbo].[Encargados]
					  WHERE ID = @aID     --Se elimina por IDs no por el foreing key 
			
			set @aMensajeError = 'Se Eliminó correctamente.'
			set @anumErr = 1;
		END

		----Actualizar una Relación
		IF (@aOperacion='u')
		BEGIN
				if(	@aIDchildRelation=-1)
					BEGIN
						set @aMensajeError = 'El Encargado no tiene relación  con el niño.'
						set @anumErr = 0;
						return 0;
					END
				
				if(	@aDNI ='')
					BEGIN
						set @aMensajeError = ' No puede ir vacio.'
						set @anumErr = 0;
						return 0;
					END

				if(	@aNombre ='')
					BEGIN
						set @aMensajeError = 'El Nombre del Encargado no puede ir vacio.'
						set @anumErr = 0;
						return 0;
					END

				if(	@aDireccion ='')
					BEGIN
						set @aMensajeError = 'La Dirección del Encargado no puede ir vacio.'
						set @anumErr = 0;
						return 0;
					END

				if(	@aTelefono ='')
					BEGIN
						set @aMensajeError = 'El Télefono del Encargado no puede ir vacio.'
						set @anumErr = 0;
						return 0;
					END
				
				--Cambios, no vamos a cambiar el DNI
				UPDATE [dbo].[Encargados]
					SET 
				   [IDchildRelation] = @aIDchildRelation
				  ,[Nombre] = @aNombre,
				  [Direccion] = @aDireccion,
				  [Telefono] = @aTelefono
					 
					WHERE ID = @aID     --Se ACTUALIZA por IDs no por el foreing key 
			  
			set @aMensajeError = 'Se Actualizó correctamente.'
			set @anumErr = 1;
	 		
		END

		-----
		IF (@aOperacion='l')
		BEGIN
				SELECT  
				   [IDchildRelation],
				   [Nombre],
				   [Direccion],
				   [Telefono]
				  			 
			  FROM [dbGuarderia].[dbo].[Encargados] as e inner join dbo.childsRelations as c on e.IDchildRelation=c.ID
			  where e.IDchildRelation=c.ID
			  
			set @aMensajeError = 'Se listó correctamente.'
			set @anumErr = 1;
			
		END
		
		IF (@aOperacion='b')
		BEGIN
			SELECT  
				   [IDchildRelation],
				   [Nombre],
				   [Direccion],
				   [Telefono]
				  			 
			  FROM [dbGuarderia].[dbo].[Encargados]  as e inner join dbo.childsRelations as c on e.IDchildRelation=c.ID
			  where e.IDchildRelation=c.ID and
			  
			  [IDchildRelation] like '%'+ @aIDchildRelation+'%' and
			   [Nombre] like '%'+ @aNombre+'%' and  [Direccion] like '%'+ @aDireccion+'%' 
			   and  [Telefono] like '%'+ @aTelefono+'%'

			   
			set @aMensajeError = 'Se buscó correctamente.'
			set @anumErr = 1;
		END


		IF (@aOperacion='g')
		BEGIN
				if(	@aID =-1)
					BEGIN
						set @aMensajeError = 'El ID de la Relación no puede ir vacio.'
						return 0;
					END
			
			SELECT  
				   [IDchildRelation],
				   [Nombre],
				   [Direccion],
				   [Telefono]
				  			 
			  FROM [dbGuarderia].[dbo].[Encargados] 
			  where  ID = @aID     --Se obtiene por IDs no por el foreing key 			  
			set @aMensajeError = 'Se obtuvo correctamente por el ID' + CONVERT (VARCHAR(MAX) ,@aID);
			set @anumErr = 1;
		END
END
