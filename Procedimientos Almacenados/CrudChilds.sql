USE [dbGuarderia]
GO
/****** Object:  StoredProcedure [dbo].[stp_CPABONADOS_ACTUALIZAR]    Script Date: 15/02/2021 11:11:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create or alter  procedure [dbo].[stp_CPChilds_CRUD] (

@aIDmatricula int =-1,
@aNombre varchar(150)='' ,
@aFechaRegistro datetime='2000-01-01',
@aFechaNacimiento datetime='2001-01-01',
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
				----Insertar un Abonado
		IF (@aOperacion='i')
			BEGIN
			if(	@aIDmatricula =-1)
					BEGIN
						set @aMensajeError = 'Niño no registrado.'
						set @anumErr = 0;
						return 0;
					END

				if(	@aNombre ='')
					BEGIN
						set @aMensajeError = 'El Nombre no puede ir vacio.'
						set @anumErr = 0;
						return 0;
					END

				if(	@aFechaNacimiento ='')
					BEGIN
						set @aMensajeError = 'La fecha de nacimiento no puede ir vacio.'
						set @anumErr = 0;
						return 0;
					END

		
				INSERT INTO [dbo].[Childs]
				  ([Nombre]
				  ,[FechaNacimiento])
				 VALUES
                  (@aNombre
				  ,@aFechaNacimiento)


					set @aMensajeError = 'El niño se ha agregado con éxito.'
						set @anumErr = 1;
					return 1
			END

	
		----Borrar un Abonado
		IF (@aOperacion='d')
		BEGIN
		--Hay que eliminar por el ID, EL ID es el Primary key
			if(	@aIDmatricula =-1)
					BEGIN
						set @aMensajeError = 'El ID del niño es nulo, 0 registros afectados.'
						set @anumErr = 0;
						return 0;
					END
			
				DELETE FROM [dbo].[Childs]
					  WHERE IDmatricula = @aIDmatricula     --Se elimina por IDs no por el foreing key 
			
			set @aMensajeError = 'Se Eliminó correctamente.'
			set @anumErr = 1;
		END

		----Actualizar un Abonado
		IF (@aOperacion='u')
		BEGIN
				if(	@aIDmatricula =-1)
					BEGIN
						set @aMensajeError = 'El ID del niño es nulo, 0 registros afectados.'
						set @anumErr = 0;
						return 0;
					END

					if(	@aNombre ='')
					BEGIN
						set @aMensajeError = 'El Nombre no puede ir vacio.'
						set @anumErr = 0;
						return 0;
					END

				if(	@aFechaNacimiento ='')
					BEGIN
						set @aMensajeError = 'La fecha de nacimiento no puede ir vacio.'
						set @anumErr = 0;
						return 0;	
					END

				--Cambios, no vamos a cambiar el DNI
				UPDATE [dbo].[Childs]
					SET 
				   [NOMBRE] = @aNombre
				  ,[FechaNacimiento] = @aFechaNacimiento
					  WHERE IDmatricula = @aIDmatricula     --Se ACTUALIZA por IDs no por el foreing key 
			  
			set @aMensajeError = 'Se Actualizó correctamente.'
			set @anumErr = 1;
	 		
		END

		-----
		IF (@aOperacion='l')
		BEGIN

			SELECT [IDmatricula]
				,[Nombre]
				,[FechaRegistro]
				,[FechaNacimiento]
		    FROM [dbo].[Childs]
		  
			set @aMensajeError = 'Se listó correctamente.'
			set @anumErr = 1;
			
		END
		
		IF (@aOperacion='b')
		BEGIN

			SELECT  
				   [IDmatricula]
				  ,[Nombre]
				  ,[FechaRegistro]
				  ,[FechaNacimiento]

			  FROM [dbGuarderia].[dbo].[Childs]
			   WHERE 
			   [IDmatricula] like '%'+ @aIDmatricula+'%' and
			   [Nombre] like '%'+ @aNombre+'%' and
			   [FechaRegistro] like '%'+ @aFechaRegistro+'%' and
			   [FechaNacimiento] like '%'+ @aFechaNacimiento+'%' 
	   
			set @aMensajeError = 'Se buscó correctamente.'
			set @anumErr = 1;
		END


		IF (@aOperacion='g')
		BEGIN
				if(	@aIDmatricula =-1)
					BEGIN
						set @aMensajeError = 'El ID del niño no puede ir vacio.'
						return 0;
					END
			
			SELECT 			
					IDmatricula ----Se necesita el ID en todo momento
				  ,[Nombre]
				  ,[FechaRegistro]
				  ,[FechaNacimiento]

			  FROM [dbGuarderia].[dbo].[Childs]
					  WHERE IDmatricula = @aIDmatricula     --Se obtiene por IDs no por el foreing key 
			  
			set @aMensajeError = 'Se obtuvo correctamente por el ID' + CONVERT (VARCHAR(MAX) ,@aIDmatricula);
			set @anumErr = 1;
		END
		
END	