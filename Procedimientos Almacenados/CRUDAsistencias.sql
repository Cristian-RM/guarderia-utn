USE [dbGuarderia]
GO
/****** Object:  StoredProcedure [dbo].[stp_CPASISTENCIAS_ACTUALIZAR]    Script Date: 15/02/2021 11:11:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create or alter  procedure [dbo].[stp_CPAASISTENCIAS_CRUD] (

@aID int =-1,
@aIDchild int =-1,
@aFechaRegistro datetime ,
@aMES varchar(25)='' ,
@aHoraEntrada time(7) ='',
@aHoraSalida time(7)= '',
@aDetalles varchar(100)= '',
@aSNCANCELADO bit = 1,
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
				----Insertar una Asistencias
		IF (@aOperacion='i')
			BEGIN
			if(	@aIDchild =-1)
					BEGIN
						set @aMensajeError = 'La Asistencia debe estar asociado a un niño.'
						set @anumErr = 0;
						return 0;
					END
				if(	@aFechaRegistro ='')
					BEGIN
						set @aMensajeError = 'La Fecha de Registro no puede ir vacio.'
						set @anumErr = 0;
						return 0;
					END
				if(	@aMES ='')
					BEGIN
						set @aMensajeError = 'El MES no puede ir vacio.'
						set @anumErr = 0;
						return 0;
					END
				
				if(	@aHoraEntrada ='')
					BEGIN
						set @aMensajeError = 'La Hora de Entrada  no puede ir vacio.'
						set @anumErr = 0;
						return 0;
					END
				if(	@aHoraSalida ='')
					BEGIN
						set @aMensajeError = 'La Hora de Salida no puede ir vacio.'
						set @anumErr = 0;
						return 0;
					END
				if(	@aDetalles ='')
					BEGIN
						set @aMensajeError = 'Los detalles no pueden ir vacios.'
						set @anumErr = 0;
						return 0;
					END

				INSERT INTO [dbo].[Asistencias]

					   ([IDchild]
					   ,[FechaRegistro]
					   ,[MES]
					   ,[HoraEntrada]
					   ,[HoraSalida]
					   ,[Detalles]
					   ,[SNCANCELADO])
				 VALUES
					   (@aIDchild
					   ,@aFechaRegistro
					   ,@aMES
					   ,@aHoraEntrada
					   ,@aHoraSalida
					   ,@aDetalles
					   ,@aSNCANCELADO)
				
					set @aMensajeError = 'La Asistencia se ha agregado con éxito.'
						set @anumErr = 1;
					return 1
			END

	
		----Borrar una Asistencia
		IF (@aOperacion='d')
		BEGIN
		--Hay que eliminar por el ID, EL ID es el Primary key
			if(	@aID =-1)
					BEGIN
						set @aMensajeError = 'El ID de la Asistencia  es nulo, 0 registros afectados.'
						set @anumErr = 0;
						return 0;
					END
			
				DELETE FROM [dbo].[Asistencias]
					  WHERE ID = @aID     --Se elimina por IDs no por el foreing key 
			
			set @aMensajeError = 'Se Eliminó correctamente.'
			set @anumErr = 1;
		END

		----Actualizar una Asistencia
		IF (@aOperacion='u')
		BEGIN
				if(	@aID =-1)
					BEGIN
						set @aMensajeError = 'El ID de la Asistencia es nulo, 0 registros afectados.'
						set @anumErr = 0;
						return 0;
						END
				if(	@aFechaRegistro ='')
					BEGIN
						set @aMensajeError = 'La Fecha de Registro no puede ir vacio.'
						set @anumErr = 0;
						return 0;
					END
				if(	@aMES ='')
					BEGIN
						set @aMensajeError = 'El MES no puede ir vacio.'
						set @anumErr = 0;
						return 0;
					END
				
				if(	@aHoraEntrada ='')
					BEGIN
						set @aMensajeError = 'La Hora de Entrada  no puede ir vacio.'
						set @anumErr = 0;
						return 0;
					END
				if(	@aHoraSalida ='')
					BEGIN
						set @aMensajeError = 'La Hora de Salida no puede ir vacio.'
						set @anumErr = 0;
						return 0;
					END
				if(	@aDetalles ='')
					BEGIN
						set @aMensajeError = 'Los detalles no pueden ir vacios.'
						set @anumErr = 0;
						return 0;
					END

				--Cambios, no vamos a cambiar el DNI
				UPDATE [dbo].[Asistencias]
					SET 
				   [FechaRegistro] = @aFechaRegistro
				  ,[MES] = @aMES
				  ,[HoraEntrada] = @aHoraEntrada
				  ,[HoraSalida] = @aHoraSalida
				  ,[Detalles] = @aDetalles
				  ,[SNCANCELADO] = @aSNCANCELADO
					  WHERE ID = @aID     --Se ACTUALIZA por IDs no por el foreing key 
			  
			set @aMensajeError = 'Se Actualizó correctamente.'
			set @anumErr = 1;
	 		
		END

		-----
		IF (@aOperacion='l')
		BEGIN
				SELECT  
				   [ID]
				  ,[IDchild]
				  ,[FechaRegistro]
				  ,[MES]
				  ,[HoraEntrada]
				  ,[HoraSalida]
				  ,[Detalles]
				  ,[SNCANCELADO]
				 
			  FROM [dbGuarderia].[dbo].[Asistencias]
			  
			set @aMensajeError = 'Se listó correctamente.'
			set @anumErr = 1;
			
		END
		
		IF (@aOperacion='b')
		BEGIN

			SELECT  
				   [ID]
				  ,[IDchild]
				  ,[FechaRegistro]
				  ,[MES]
				  ,[HoraEntrada]
				  ,[HoraSalida]
				  ,[Detalles]
				  ,[SNCANCELADO]
			  FROM [dbGuarderia].[dbo].[Asistencias]
			   WHERE 
			   [IDchild] like '%'+ @aIDchild+'%' and
			   [FechaRegistro] like '%'+ @aFechaRegistro+'%' and
			   [MES] like '%'+ @aMES+'%' and
			   [HoraEntrada] like '%'+ @aHoraEntrada+'%' and
			   [HoraSalida] like '%'+ @aHoraSalida+'%' and
			   [Detalles] like '%'+ @aDetalles+'%' and
			   [SNCANCELADO] like '%'+ @aSNCANCELADO+'%' 

			   
			set @aMensajeError = 'Se buscó correctamente.'
			set @anumErr = 1;
		END


		IF (@aOperacion='g')
		BEGIN
				if(	@aID =-1)
					BEGIN
						set @aMensajeError = 'El ID de la Asistencia no puede ir vacio.'
						return 0;
					END
			
			SELECT 			
					ID ----Se necesita el ID en todo momento
				  ,[IDchild]
				  ,[FechaRegistro]
				  ,[MES]
				  ,[HoraEntrada]
				  ,[HoraSalida]
				  ,[Detalles]
				  ,[SNCANCELADO]
			  FROM [dbGuarderia].[dbo].[Asistencias]
					  WHERE ID = @aID     --Se obtiene por IDs no por el foreing key 
			  
			set @aMensajeError = 'Se obtuvo correctamente por el ID' + CONVERT (VARCHAR(MAX) ,@aID);
			set @anumErr = 1;
		END

END	