USE [dbGuarderia]
GO
/****** Object:  StoredProcedure [dbo].[stp_CPABONADOS_ACTUALIZAR]    Script Date: 15/02/2021 11:11:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create or alter  procedure [dbo].[stp_CPDetalleConsumos_CRUD] (

@aID int =-1,
@aIDfactura int =-1,
@aIDconsumo int =-1,
@aFechaCreacion datetime='',
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
			if(	@aIDfactura=-1)
					BEGIN
						set @aMensajeError = 'La Factura no tiene relación  con el Consumo.'
						set @anumErr = 0;
						return 0;
					END
				
				if(	@aIDconsumo =-1)
					BEGIN
						set @aMensajeError = 'El Consumo no tiene relación  con el Detalle.'
						set @anumErr = 0;
						return 0;
					END

					INSERT INTO [dbo].[DetalleConsumos]
						 ([IDfactura],
						 [IDconsumo],
					    [FechaCreacion])
   
				 VALUES
					   (@aIDfactura,
					    @aIDconsumo,
					   @aFechaCreacion)
				
					set @aMensajeError = 'El Detalle Consumo se ha agregado con éxito.'
						set @anumErr = 1;
					return 1
			END

	
		----Borrar un Relación
		IF (@aOperacion='d')
		BEGIN
		--Hay que eliminar por el ID, EL ID es el Primary key
			if(	@aID =-1)
					BEGIN
						set @aMensajeError = 'El ID del detalle es nulo, 0 registros afectados.'
						set @anumErr = 0;
						return 0;
					END
			
				DELETE FROM [dbo].[DetalleConsumos]
					  WHERE ID = @aID     --Se elimina por IDs no por el foreing key 
			
			set @aMensajeError = 'Se Eliminó correctamente.'
			set @anumErr = 1;
		END

		----Actualizar un Detalle
		IF (@aOperacion='u')
		BEGIN
				
				if(	@aIDfactura=-1)
					BEGIN
						set @aMensajeError = 'La Factura no tiene relación  con el Consumo.'
						set @anumErr = 0;
						return 0;
					END
				
				if(	@aIDconsumo =-1)
					BEGIN
						set @aMensajeError = 'El Consumo no tiene relación  con el Detalle.'
						set @anumErr = 0;
						return 0;
					END
				
				--Cambios, no vamos a cambiar el DNI
				UPDATE [dbo].[DetalleConsumos]
					SET 
				   [IDfactura] = @aIDfactura
				  ,[IDconsumo] = @aIDconsumo,
				  [FechaCreacion] = @aFechaCreacion
					 
					WHERE ID = @aID     --Se ACTUALIZA por IDs no por el foreing key 
			  
			set @aMensajeError = 'Se Actualizó correctamente.'
			set @anumErr = 1;
	 		
		END

		-----
		IF (@aOperacion='l')
		BEGIN
				SELECT  
				   [ID],
				   [IDfactura],
				   [IDconsumo],
				   [FechaCreacion]
			  FROM [dbGuarderia].[dbo].[DetalleConsumos]
			  
			set @aMensajeError = 'Se listó correctamente.'
			set @anumErr = 1;
			
		END
		
		IF (@aOperacion='b')
		BEGIN
			SELECT  
				   [ID],
				   [IDfactura],
				   [IDconsumo],
				   [FechaCreacion]
				  			 
			  FROM [dbGuarderia].[dbo].[DetalleConsumos] where
			  
			  [ID] like '%'+ @aID+'%' and
			   [IDfactura] like '%'+ @aIDfactura+'%' and  [IDconsumo] like '%'+ @aIDconsumo+'%' 
			   and  [FechaCreacion] like '%'+ @aFechaCreacion+'%'

			   
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
				   [IDfactura],
				   [IDconsumo],
				   [FechaCreacion]
				  			 
			  FROM [dbGuarderia].[dbo].[DetalleConsumos] 
			  where  ID = @aID     --Se obtiene por IDs no por el foreing key 			  
			set @aMensajeError = 'Se obtuvo correctamente por el ID' + CONVERT (VARCHAR(MAX) ,@aID);
			set @anumErr = 1;
		END
END
