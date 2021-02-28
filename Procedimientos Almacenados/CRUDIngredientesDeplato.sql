USE [dbGuarderia]
GO
/****** Object:  StoredProcedure [dbo].[stp_CPINGREDIENTESDEPLATO_ACTUALIZAR]    Script Date: 15/02/2021 11:11:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create or alter  procedure [dbo].[stp_CPINGREDIENTESDEPLATO_CRUD] (

@aID int =-1,
@aNombrePlato varchar(50) = '',
@aNombreIngrediente varchar(50)= '' ,
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
				----Insertar un Ingrediente del plato
		IF (@aOperacion='i')
			BEGIN
			if(	@aID =-1)
					BEGIN
						set @aMensajeError = 'El ingrediente de Plato debe de estar asosciado con ingredientes.'
						set @anumErr = 0;
						return 0;
					END
				if(	@aNombrePlato ='')
					BEGIN
						set @aMensajeError = 'El Nombre del plato no puede ir vacio.'
						set @anumErr = 0;
						return 0;
					END
				if(	@aNombreIngrediente ='')
					BEGIN
						set @aMensajeError = 'El Nombre del Ingrediente no puede ir vacio.'
						set @anumErr = 0;
						return 0;
					END
			

				INSERT INTO [dbo].[IngredientesDeplato]

					   ([ID]
					   ,[NombrePlato]
					   ,[NombreIngrediente]
						)
				 VALUES
					   (@aID
					   ,@aNombrePlato
					   ,@aNombreIngrediente)
				
					set @aMensajeError = 'El Ingrediente del plato se ha agregado con éxito.'
						set @anumErr = 1;
					return 1
			END

	
		----Borrar un Ingrediente del plato
		IF (@aOperacion='d')
		BEGIN
		--Hay que eliminar por el ID, EL ID es el Primary key
			if(	@aID =-1)
					BEGIN
						set @aMensajeError = 'El Ingrediente del plato  es nulo, 0 registros afectados.'
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
						set @aMensajeError = 'El ingrediente de Plato debe de estar asosciado con ingredientes.'
						set @anumErr = 0;
						return 0;
					END
				if(	@aNombrePlato ='')
					BEGIN
						set @aMensajeError = 'El Nombre del plato no puede ir vacio.'
						set @anumErr = 0;
						return 0;
					END
				if(	@aNombreIngrediente ='')
					BEGIN
						set @aMensajeError = 'El Nombre del Ingrediente no puede ir vacio.'
						set @anumErr = 0;
						return 0;
					END
				

				--Cambios, no vamos a cambiar el DNI
				UPDATE [dbo].[IngredientesDeplato]
					SET 
				   [NombrePlato] = @aNombrePlato
				  ,[NombreIngrediente] = @aNombreIngrediente
				  
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
				  ,[NombreIngrediente]
				 
				 
			  FROM [dbGuarderia].[dbo].[IngredientesDeplato]
			  
			set @aMensajeError = 'Se listó correctamente.'
			set @anumErr = 1;
			
		END
		
		IF (@aOperacion='b')
		BEGIN

			SELECT  
				   [ID]
				  ,[NombrePlato]
				  ,[NombreIngrediente]
				  
				 
			  FROM [dbGuarderia].[dbo].[IngredientesDeplato]
			   WHERE 
			   [ID] like '%'+ @aID+'%' and
			   [NombrePlato] like '%'+ @aNombrePlato+'%' and
			   [NombreIngrediente] like '%'+ @aNombreIngrediente+'%' 
			

			   
			set @aMensajeError = 'Se buscó correctamente.'
			set @anumErr = 1;
		END


		IF (@aOperacion='g')
		BEGIN
				if(	@aID =-1)
					BEGIN
						set @aMensajeError = 'El ID del ingrediente del plato no puede ir vacio.'
						return 0;
					END
			
			SELECT 			
					ID ----Se necesita el ID en todo momento
				  ,[NombrePlato]
				  ,[NombreIngrediente]
				  
			  FROM [dbGuarderia].[dbo].[IngredientesDeplato]
					  WHERE ID = @aID     --Se obtiene por IDs no por el foreing key 
			  
			set @aMensajeError = 'Se obtuvo correctamente por el ID' + CONVERT (VARCHAR(MAX) ,@aID);
			set @anumErr = 1;
		END

END	