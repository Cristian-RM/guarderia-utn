USE [dbGuarderia]
GO
/****** Object:  StoredProcedure [dbo].[stp_CPABONADOS_ACTUALIZAR]    Script Date: 15/02/2021 11:11:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create or alter  procedure [dbo].[stp_CPConsumos_CRUD] (

@aID int =-1,
@aIDchild int=-1,
@aIDmenu int=-1,
@aFechaConsumo datetime='2000-01-01',
@aSnCancelado bit= 1,
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

				if(	@aIDchild ='')
					BEGIN
						set @aMensajeError = 'El consumo debe estar asignado a un niño.'
						set @anumErr = 0;
						return 0;
					END

				if(	@aIDchild ='')
					BEGIN
						set @aMensajeError ='El consumo debe estar asignado a un Menú.'
						set @anumErr = 0;
						return 0;
					END

					INSERT INTO [dbo].[Consumos]
						 ([IDchild],
						 [IDmenu],
						 [SNCANCELADO])
   
				 VALUES
					   (@aIDchild,
						@aIDmenu,
						@aSnCancelado)
				
					set @aMensajeError = 'El Consumo se ha agregado con éxito.'
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
			
				DELETE FROM [dbo].[Consumos]
					  WHERE ID = @aID     --Se elimina por IDs no por el foreing key 
			
			set @aMensajeError = 'Se Eliminó correctamente.'
			set @anumErr = 1;
		END

		----Actualizar una Relación
		IF (@aOperacion='u')
		BEGIN
				if(	@aIDchild ='')
					BEGIN
						set @aMensajeError = 'El consumo debe estar asignado a un niño.'
						set @anumErr = 0;
						return 0;
					END

				if(	@aIDchild ='')
					BEGIN
						set @aMensajeError ='El consumo debe estar asignado a un Menú.'
						set @anumErr = 0;
						return 0;
					END
				
				--Cambios, no vamos a cambiar el DNI
				UPDATE [dbo].[Consumos]
					SET 
				  [IDchild] = @aIDchild,
				  [IDmenu] = @aIDmenu,
				  [SNCANCELADO] = @aSnCancelado

					 
					WHERE ID = @aID     --Se ACTUALIZA por IDs no por el foreing key 
			  
			set @aMensajeError = 'Se Actualizó correctamente.'
			set @anumErr = 1;
	 		
		END

		-----
		IF (@aOperacion='l')
		BEGIN
				SELECT  
				   [ID],
				   [IDchild],
				   [IDmenu],
				   [FechaConsumo],
				   [SNCANCELADO]
				  			 
			  FROM [dbGuarderia].[dbo].[Consumos] 
			set @aMensajeError = 'Se listó correctamente.'
			set @anumErr = 1;
			
		END
		
		IF (@aOperacion='b')
		BEGIN
			SELECT  
				   [ID],
				   [IDchild],
				   [IDmenu],
				   [FechaConsumo],
				   [SNCANCELADO]
				  			 
			  FROM [dbGuarderia].[dbo].[Consumos]  where
			  
			  [ID] like '%'+ @aID+'%' and
			   [IDchild] like '%'+ @aIDchild+'%' and  [IDmenu] like '%'+ @aIDmenu+'%' and
			   [FechaConsumo] like '%'+ @aFechaConsumo+'%' 
			   
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
				   [IDchild],
				   [IDmenu],
				   [FechaConsumo],
				   [SNCANCELADO]
				  			 
			  FROM [dbGuarderia].[dbo].[Consumos] 
			  where  ID = @aID     --Se obtiene por IDs no por el foreing key 			  
			set @aMensajeError = 'Se obtuvo correctamente por el ID' + CONVERT (VARCHAR(MAX) ,@aID);
			set @anumErr = 1;
		END
END


