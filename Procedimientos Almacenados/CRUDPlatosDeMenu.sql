USE [dbGuarderia]
GO
/****** Object:  StoredProcedure [dbo].[stp_CPPLATOSDEMENU_ACTUALIZAR]    Script Date: 15/02/2021 11:11:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create or alter  procedure [dbo].[stp_CPPLATOSDEMENU_CRUD] (

@aID int =-1,
@aNombrePlato varchar(50) = '',
@aIDmenu int= -1 ,
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
				----Insertar un Plato de Menu
		IF (@aOperacion='i')
			BEGIN
			if(	@aID =-1)
					BEGIN
						set @aMensajeError = 'El Plato de Menu debe de estar asosciado con Platos.'
						set @anumErr = 0;
						return 0;
					END
				if(	@aNombrePlato ='')
					BEGIN
						set @aMensajeError = 'El Nombre del plato no puede ir vacio.'
						set @anumErr = 0;
						return 0;
					END
			

				INSERT INTO [dbo].[platosDemenu]

					   ([ID]
					   ,[NombrePlato]
					   ,[IDmenu]
						)
				 VALUES
					   (@aID
					   ,@aNombrePlato
					   ,@aIDmenu)
				
					set @aMensajeError = 'El Plato del Menu se ha agregado con éxito.'
						set @anumErr = 1;
					return 1
			END

	
		----Borrar un Plato del menu
		IF (@aOperacion='d')
		BEGIN
		--Hay que eliminar por el ID, EL ID es el Primary key
			if(	@aID =-1)
					BEGIN
						set @aMensajeError = 'El  plato del Menu es nulo, 0 registros afectados.'
						set @anumErr = 0;
						return 0;
					END
			
				DELETE FROM [dbo].[IngredientesDeplato]
					  WHERE ID = @aID     --Se elimina por IDs no por el foreing key 
			
			set @aMensajeError = 'Se Eliminó correctamente.'
			set @anumErr = 1;
		END

		----Actualizar un Ingrediente del plato
		IF (@aOperacion='u')
		BEGIN
			if(	@aID =-1)
					BEGIN
						set @aMensajeError = 'El Plato de Menu debe de estar asosciado con Platos.'
						set @anumErr = 0;
						return 0;
					END
				if(	@aNombrePlato ='')
					BEGIN
						set @aMensajeError = 'El Nombre del plato no puede ir vacio.'
						set @anumErr = 0;
						return 0;
					END

				--Cambios, no vamos a cambiar el DNI
				UPDATE [dbo].[platosDemenu]
					SET 
				   [NombrePlato] = @aNombrePlato
				  ,[IDmenu] = @aIDmenu
				  
					  WHERE ID = @aID     --Se ACTUALIZA por IDs no por el foreing key 
			  
			set @aMensajeError = 'Se Actualizó correctamente.'
			set @anumErr = 1;
	 		
		END

		-----
		IF (@aOperacion='l')
		BEGIN
				SELECT  
				   [ID]
				  ,[NombrePlato]
				  ,[IDmenu]
				 
				 
			  FROM [dbGuarderia].[dbo].[platosDemenu]
			  
			set @aMensajeError = 'Se listó correctamente.'
			set @anumErr = 1;
			
		END
		
		IF (@aOperacion='b')
		BEGIN

			SELECT  
				   [ID]
				  ,[NombrePlato]
				  ,[IDmenu]
				  
				 
			  FROM [dbGuarderia].[dbo].[platosDemenu]
			   WHERE 
			   [ID] like '%'+ @aID+'%' and
			   [NombrePlato] like '%'+ @aNombrePlato+'%' and
			   [IDmenu] like '%'+ @aIDmenu+'%' 
			

			   
			set @aMensajeError = 'Se buscó correctamente.'
			set @anumErr = 1;
		END


		IF (@aOperacion='g')
		BEGIN
				if(	@aID =-1)
					BEGIN
						set @aMensajeError = 'El ID del Plato de Menu del plato no puede ir vacio.'
						return 0;
					END
			
			SELECT 			
					ID ----Se necesita el ID en todo momento
				  ,[NombrePlato]
				  ,[IDmenu]
				  
			  FROM [dbGuarderia].[dbo].[platosDemenu]
					  WHERE ID = @aID     --Se obtiene por IDs no por el foreing key 
			  
			set @aMensajeError = 'Se obtuvo correctamente por el ID' + CONVERT (VARCHAR(MAX) ,@aID);
			set @anumErr = 1;
		END

END	