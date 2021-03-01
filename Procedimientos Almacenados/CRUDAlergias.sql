
/****** Object:  StoredProcedure [dbo].[stp_CPALERGIAS_ACTUALIZAR]    Script Date: 15/02/2021 11:11:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create or alter  procedure [dbo].[stp_CPALERGIAS_CRUD] (

@aID int =-1,
@aNombreIngrediente varchar(50)= '',
@aIDchild int =-1,
@pOperacion varchar(1) = 'n',
@pMensajeError varchar(max) = 'no definido' output  ,
@pnumErr int= -1 output  
)
as 
BEGIN

		if(@pOperacion = 'n')
			BEGIN
				return 0;
			END
				----Insertar una Alergia
		IF (@pOperacion='i')
			BEGIN
			if(	@aID=-1)
					BEGIN
						set @pMensajeError = 'El ID no esta asociado a Ingredientes.'
						set @pnumErr = 0;
						return 0;
					END
				
				if(	@aNombreIngrediente ='')
					BEGIN
						set @pMensajeError = 'El Consumo no tiene relación  con el Detalle.'
						set @pnumErr = 0;
						return 0;
					END
				if(	@aIDchild = -1)
					BEGIN
						set @pMensajeError = 'El IDChild no está asosciado a Alergias.'
						set @pnumErr = 0;
						return 0;
					END

					INSERT INTO [dbo].[Alergias]
						
						 ([NombreIngrediente],
					    [IDchild])
   
				 VALUES
					   (
					    @aNombreIngrediente,
					   @aIDchild)
				
					set @pMensajeError = 'La Alergia se ha agregado con éxito.'
						set @pnumErr = 1;
					return 1
			END

	
		----Borrar una Alergia
		IF (@pOperacion='d')
		BEGIN
		--Hay que eliminar por el ID, EL ID es el Primary key
			if(	@aID =-1)
					BEGIN
						set @pMensajeError = 'El ID de Alergias es nulo, 0 registros afectados.'
						set @pnumErr = 0;
						return 0;
					END
			
				DELETE FROM [dbo].[Alergias]
					  WHERE ID = @aID     --Se elimina por IDs no por el foreing key 
			
			set @pMensajeError = 'Se Eliminó correctamente.'
			set @pnumErr = 1;
		END

		----Actualizar una Alergia
		IF (@pOperacion='u')
		BEGIN
				
				if(	@aID=-1)
					BEGIN
						set @pMensajeError = 'El ID no esta asociado a Ingredientes.'
						set @pnumErr = 0;
						return 0;
					END
				
				if(	@aNombreIngrediente ='')
					BEGIN
						set @pMensajeError = 'El Consumo no tiene relación  con el Detalle.'
						set @pnumErr = 0;
						return 0;
					END
				if(	@aIDchild = -1)
					BEGIN
						set @pMensajeError = 'El IDChild no está asosciado a Alergias.'
						set @pnumErr = 0;
						return 0;
					END
				
				--Cambios, no vamos a cambiar el DNI
				UPDATE [dbo].[Alergias]
					SET 
				   [NombreIngrediente] = @aNombreIngrediente
				  ,[IDchild] = @aIDchild
					 
					WHERE ID = @aID     --Se ACTUALIZA por IDs no por el foreing key 
			  
			set @pMensajeError = 'Se Actualizó correctamente.'
			set @pnumErr = 1;
	 		
		END

		-----
		IF (@pOperacion='l')
		BEGIN
				SELECT  
				   [ID],
				   [NombreIngrediente],
				   [IDchild]
			  FROM [dbo].[Alergias]
			  
			set @pMensajeError = 'Se listó correctamente.'
			set @pnumErr = 1;
			
		END
		
		IF (@pOperacion='b')
		BEGIN
			SELECT  
				   [ID],
				   [NombreIngrediente],
				   [IDchild]
				  			 
			  FROM [dbo].[Alergias] where
			  
			  [ID] like '%'+ @aID+'%' and
			   [NombreIngrediente] like '%'+ @aNombreIngrediente+'%' and 
			   [IDchild] like '%'+ @aIDchild+'%' 
			  
			   
			set @pMensajeError = 'Se buscó correctamente.'
			set @pnumErr = 1;
		END


		IF (@pOperacion='g')
		BEGIN
				if(	@aID =-1)
					BEGIN
						set @pMensajeError = 'El ID de la Relación no puede ir vacio.'
						return 0;
					END
			
			SELECT  
				   [ID],
				   [NombreIngrediente],
				   [IDchild]
				  			 
			  FROM [dbo].[Alergias] 
			  where  ID = @aID     --Se obtiene por IDs no por el foreing key 			  
			set @pMensajeError = 'Se obtuvo correctamente por el ID' + CONVERT (VARCHAR(MAX) ,@aID);
			set @pnumErr = 1;
		END
END
