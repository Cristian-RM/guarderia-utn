USE [dbGuarderia]
GO
/****** Object:  StoredProcedure [dbo].[stp_CPABONADOS_ACTUALIZAR]    Script Date: 15/02/2021 11:11:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create or alter  procedure [dbo].[stp_CPMfacturas_CRUD] (

@aID int =-1,
@aNombre varchar(50)='', 
@aPrecio decimal(10,2)=0,
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

				if(	@aNombre ='')
					BEGIN
						set @aMensajeError = 'El Nombre del menú no puede ir vacio.'
						set @anumErr = 0;
						return 0;
					END

				if(	@aPrecio =-1)
					BEGIN
						set @aMensajeError = 'EL precio del menú no puede ir vacio.'
						set @anumErr = 0;
						return 0;
					END

					INSERT INTO [dbo].[Menus]
						 ([Nombre],
						 [Precio])
   
				 VALUES
					   (@aNombre,
						@aPrecio)
				
					set @aMensajeError = 'El Menú se ha agregado con éxito.'
						set @anumErr = 1;
					return 1
			END

	
		----Borrar un Relación
		IF (@aOperacion='d')
		BEGIN
		--Hay que eliminar por el ID, EL ID es el Primary key
			if(	@aID =-1)
					BEGIN
						set @aMensajeError = 'El ID del Menú es nulo, 0 registros afectados.'
						set @anumErr = 0;
						return 0;
					END
			
				DELETE FROM [dbo].[Menus]
					  WHERE ID = @aID     --Se elimina por IDs no por el foreing key 
			
			set @aMensajeError = 'Se Eliminó correctamente.'
			set @anumErr = 1;
		END

		----Actualizar una Relación
		IF (@aOperacion='u')
		BEGIN
				if(	@aNombre ='')
					BEGIN
						set @aMensajeError = 'El Nombre del menú no puede ir vacio.'
						set @anumErr = 0;
						return 0;
					END

				if(	@aPrecio =-1)
					BEGIN
						set @aMensajeError = 'EL precio del menú no puede ir vacio.'
						set @anumErr = 0;
						return 0;
					END

				
				--Cambios, no vamos a cambiar el DNI
				UPDATE [dbo].Menus
					SET 
				  Nombre = @aNombre,
				  Precio = @aPrecio
					 
					WHERE ID = @aID     --Se ACTUALIZA por IDs no por el foreing key 
			  
			set @aMensajeError = 'Se Actualizó correctamente.'
			set @anumErr = 1;
	 		
		END

		-----
		IF (@aOperacion='l')
		BEGIN
				SELECT  
				   [ID],
				   [Nombre],
				   [Precio]
				  			 
			  FROM [dbGuarderia].[dbo].[Menus]
			  
			set @aMensajeError = 'Se listó correctamente.'
			set @anumErr = 1;
			
		END
		
		IF (@aOperacion='b')
		BEGIN
			SELECT  	
				   [ID],
				   [Nombre],
				   [Precio]
				  			 
			  FROM [dbGuarderia].[dbo].[Menus]  where
			  
			  [ID] like '%'+ @aID+'%' and
			   [Nombre] like '%'+ @aNombre+'%' and  [Precio] like '%'+ @aPrecio+'%' 


			   
			set @aMensajeError = 'Se buscó correctamente.'
			set @anumErr = 1;
		END


		IF (@aOperacion='g')
		BEGIN
				if(	@aID =-1)
					BEGIN
						set @aMensajeError = 'El ID del Menú no puede ir vacio.'
						return 0;
					END
			
			SELECT  
				   [ID],
				   [Nombre],
				   [Precio]
				  			 
			  FROM [dbGuarderia].[dbo].[Menus] 
			  where  ID = @aID     --Se obtiene por IDs no por el foreing key 			  
			set @aMensajeError = 'Se obtuvo correctamente por el ID' + CONVERT (VARCHAR(MAX) ,@aID);
			set @anumErr = 1;
		END
END
