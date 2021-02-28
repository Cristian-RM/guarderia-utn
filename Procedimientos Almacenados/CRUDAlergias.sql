USE [dbGuarderia]
GO
/****** Object:  StoredProcedure [dbo].[stp_CPALERGIAS_ACTUALIZAR]    Script Date: 15/02/2021 11:11:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create or alter  procedure [dbo].[stp_CPALERGIAS_CRUD] (

@aID int =-1,
@aNombreIngrediente varchar(50)= '',
@aIDchild int =-1,
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
				----Insertar una Alergia
		IF (@aOperacion='i')
			BEGIN
			if(	@aID=-1)
					BEGIN
						set @aMensajeError = 'El ID no esta asociado a Ingredientes.'
						set @anumErr = 0;
						return 0;
					END
				
				if(	@aNombreIngrediente ='')
					BEGIN
						set @aMensajeError = 'El Consumo no tiene relación  con el Detalle.'
						set @anumErr = 0;
						return 0;
					END
				if(	@aIDchild = -1)
					BEGIN
						set @aMensajeError = 'El IDChild no está asosciado a Alergias.'
						set @anumErr = 0;
						return 0;
					END

					INSERT INTO [dbo].[Alergias]
						
						 ([NombreIngrediente],
					    [IDchild])
   
				 VALUES
					   (
					    @aNombreIngrediente,
					   @aIDchild)
				
					set @aMensajeError = 'La Alergia se ha agregado con éxito.'
						set @anumErr = 1;
					return 1
			END

	
		----Borrar una Alergia
		IF (@aOperacion='d')
		BEGIN
		--Hay que eliminar por el ID, EL ID es el Primary key
			if(	@aID =-1)
					BEGIN
						set @aMensajeError = 'El ID de Alergias es nulo, 0 registros afectados.'
						set @anumErr = 0;
						return 0;
					END
			
				DELETE FROM [dbo].[Alergias]
					  WHERE ID = @aID     --Se elimina por IDs no por el foreing key 
			
			set @aMensajeError = 'Se Eliminó correctamente.'
			set @anumErr = 1;
		END

		----Actualizar una Alergia
		IF (@aOperacion='u')
		BEGIN
				
				if(	@aID=-1)
					BEGIN
						set @aMensajeError = 'El ID no esta asociado a Ingredientes.'
						set @anumErr = 0;
						return 0;
					END
				
				if(	@aNombreIngrediente ='')
					BEGIN
						set @aMensajeError = 'El Consumo no tiene relación  con el Detalle.'
						set @anumErr = 0;
						return 0;
					END
				if(	@aIDchild = -1)
					BEGIN
						set @aMensajeError = 'El IDChild no está asosciado a Alergias.'
						set @anumErr = 0;
						return 0;
					END
				
				--Cambios, no vamos a cambiar el DNI
				UPDATE [dbo].[Alergias]
					SET 
				   [NombreIngrediente] = @aNombreIngrediente
				  ,[IDchild] = @aIDchild
					 
					WHERE ID = @aID     --Se ACTUALIZA por IDs no por el foreing key 
			  
			set @aMensajeError = 'Se Actualizó correctamente.'
			set @anumErr = 1;
	 		
		END

		-----
		IF (@aOperacion='l')
		BEGIN
				SELECT  
				   [ID],
				   [NombreIngrediente],
				   [IDchild]
			  FROM [dbGuarderia].[dbo].[Alergias]
			  
			set @aMensajeError = 'Se listó correctamente.'
			set @anumErr = 1;
			
		END
		
		IF (@aOperacion='b')
		BEGIN
			SELECT  
				   [ID],
				   [NombreIngrediente],
				   [IDchild]
				  			 
			  FROM [dbGuarderia].[dbo].[Alergias] where
			  
			  [ID] like '%'+ @aID+'%' and
			   [NombreIngrediente] like '%'+ @aNombreIngrediente+'%' and 
			   [IDchild] like '%'+ @aIDchild+'%' 
			  
			   
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
				   [ID],
				   [NombreIngrediente],
				   [IDchild]
				  			 
			  FROM [dbGuarderia].[dbo].[Alergias] 
			  where  ID = @aID     --Se obtiene por IDs no por el foreing key 			  
			set @aMensajeError = 'Se obtuvo correctamente por el ID' + CONVERT (VARCHAR(MAX) ,@aID);
			set @anumErr = 1;
		END
END
