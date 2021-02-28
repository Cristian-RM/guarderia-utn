USE [dbGuarderia]
GO
/****** Object:  StoredProcedure [dbo].[stp_CPABONADOS_ACTUALIZAR]    Script Date: 15/02/2021 11:11:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create or alter  procedure [dbo].[stp_CPDetalleAsistencia_CRUD] (

@aID int =-1,
@aIDfactura int =-1,
@aIDasistencia int =-1,
@aFechaCreacion datetime='2000-01-01',
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
				----Insertar una Relaci�n
		IF (@aOperacion='i')
			BEGIN
			if(	@aIDfactura=-1)
					BEGIN
						set @aMensajeError = 'La Factura no tiene relaci�n  con el Consumo.'
						set @anumErr = 0;
						return 0;
					END
				
				if(	@aIDasistencia =-1)
					BEGIN
						set @aMensajeError = 'La Asistencia no tiene relaci�n  con el Detalle.'
						set @anumErr = 0;
						return 0;
					END

					INSERT INTO [dbo].[DetalleAsistencias]
						 ([IDfactura],
						 [IDasistencia])
   
				 VALUES
					   (@aIDfactura,
					    @aIDasistencia)
				
					set @aMensajeError = 'El Detalle Asistencia se ha agregado con �xito.'
						set @anumErr = 1;
					return 1
			END

	
		----Borrar un Relaci�n
		IF (@aOperacion='d')
		BEGIN
		--Hay que eliminar por el ID, EL ID es el Primary key
			if(	@aID =-1)
					BEGIN
						set @aMensajeError = 'El ID del detalle es nulo, 0 registros afectados.'
						set @anumErr = 0;
						return 0;
					END
			
				DELETE FROM [dbo].[DetalleAsistencias]
					  WHERE ID = @aID     --Se elimina por IDs no por el foreing key 
			
			set @aMensajeError = 'Se Elimin� correctamente.'
			set @anumErr = 1;
		END

		----Actualizar un Detalle
		IF (@aOperacion='u')
		BEGIN
			if(	@aIDfactura=-1)
					BEGIN
						set @aMensajeError = 'La Factura no tiene relaci�n  con el Consumo.'
						set @anumErr = 0;
						return 0;
					END
				
				if(	@aIDasistencia =-1)
					BEGIN
						set @aMensajeError = 'La Asistencia no tiene relaci�n  con el Detalle.'
						set @anumErr = 0;
						return 0;
					END
				
				--Cambios, no vamos a cambiar el DNI
				UPDATE [dbo].[DetalleAsistencias]
					SET 
				   [IDfactura] = @aIDfactura
				  ,[IDasistencia] = @aIDasistencia,
				  [FechaCreacion] = @aFechaCreacion
					 
					WHERE ID = @aID     --Se ACTUALIZA por IDs no por el foreing key 
			  
			set @aMensajeError = 'Se Actualiz� correctamente.'
			set @anumErr = 1;
	 		
		END

		-----
		IF (@aOperacion='l')
		BEGIN
				SELECT  
				   [ID],
				   [IDfactura],
				   [IDasistencia],
				   [FechaCreacion]
			  FROM [dbGuarderia].[dbo].[DetalleAsistencias]
			  
			set @aMensajeError = 'Se list� correctamente.'
			set @anumErr = 1;
			
		END
		
		IF (@aOperacion='b')
		BEGIN
			SELECT  
				   [ID],
				   [IDfactura],
				   [IDasistencia],
				   [FechaCreacion]
				  			 
			  FROM [dbGuarderia].[dbo].[DetalleAsistencias] where
			  
			  [ID] like '%'+ @aID+'%' and
			   [IDfactura] like '%'+ @aIDfactura+'%' and  [IDasistencia] like '%'+ @aIDasistencia+'%' 
			   and  [FechaCreacion] like '%'+ @aFechaCreacion+'%'

			   
			set @aMensajeError = 'Se busc� correctamente.'
			set @anumErr = 1;
		END


		IF (@aOperacion='g')
		BEGIN
				if(	@aID =-1)
					BEGIN
						set @aMensajeError = 'El ID de la Relaci�n no puede ir vacio.'
						return 0;
					END
			
			SELECT  
				   [ID],
				   [IDfactura],
				   [IDasistencia],
				   [FechaCreacion]
				  			 
			  FROM [dbGuarderia].[dbo].[DetalleAsistencias] 
			  where  ID = @aID     --Se obtiene por IDs no por el foreing key 			  
			set @aMensajeError = 'Se obtuvo correctamente por el ID' + CONVERT (VARCHAR(MAX) ,@aID);
			set @anumErr = 1;
		END
END
