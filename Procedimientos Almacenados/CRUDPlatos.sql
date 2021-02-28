USE [dbGuarderia]
GO
/****** Object:  StoredProcedure [dbo].[stp_CPPLATOS_ACTUALIZAR]    Script Date: 15/02/2021 11:11:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create or alter  procedure [dbo].[stp_CPPLATOS_CRUD] (

@aNombre varchar(50)= '',
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
				----Insertar un Plato
		IF (@aOperacion='i')
			BEGIN
			if(	@aNombre = '')
					BEGIN
						set @aMensajeError = 'El nombre del Plato no puede estar vacio.'
						set @anumErr = 0;
						return 0;
					END
			

				INSERT INTO [dbo].[Platos]

					   ([Nombre]
					   )
				
				 VALUES
					   (@aNombre
					   )
					
				
					set @aMensajeError = 'El Plato se ha agregado con éxito.'
						set @anumErr = 1;
					return 1
			END

	
		----Borrar un Plato
		IF (@aOperacion='d')
		BEGIN
		
			if(	@aNombre = '')
					BEGIN
						set @aMensajeError = 'El Plato  es nulo, 0 registros afectados.'
						set @anumErr = 0;
						return 0;
					END
			
				DELETE FROM [dbo].[Platos]
					  WHERE Nombre = @aNombre   
			
			set @aMensajeError = 'Se Eliminó correctamente.'
			set @anumErr = 1;
		END

		----Actualizar un Plato
		IF (@aOperacion='u')
		BEGIN
				if(	@aNombre = '')
					BEGIN
						set @aMensajeError = 'El Plato es nulo, 0 registros afectados.'
						set @anumErr = 0;
						return 0;
						END
			

				
				UPDATE [dbo].[Platos]
					SET 
				   [Nombre] = @aNombre
				
					  WHERE Nombre = @aNombre    
			  
			set @aMensajeError = 'Se Actualizó correctamente.'
			set @anumErr = 1;
	 		
		END

		-----
		IF (@aOperacion='l')
		BEGIN
				SELECT  
				   [Nombre]
				
				 
			  FROM [dbGuarderia].[dbo].[Platos]
			  
			set @aMensajeError = 'Se listó correctamente.'
			set @anumErr = 1;
			
		END
		
		IF (@aOperacion='b')
		BEGIN

			SELECT  
				   [Nombre]
				  
			  FROM [dbGuarderia].[dbo].[Platos]
			   WHERE 
			   [Nombre] like '%'+ @aNombre+'%' 
			   
			set @aMensajeError = 'Se buscó correctamente.'
			set @anumErr = 1;
		END


		IF (@aOperacion='g')
		BEGIN
				if(	@aNombre =-1)
					BEGIN
						set @aMensajeError = 'El nombre del Plato no puede ir vacio.'
						return 0;
					END
			
			SELECT 			
					Nombre
				 
			  FROM [dbGuarderia].[dbo].[Platos]
					  WHERE Nombre = @aNombre     --Se obtiene por IDs no por el foreing key 
			  
			set @aMensajeError = 'Se obtuvo correctamente por el Nombre' + CONVERT (VARCHAR(MAX) ,@aNombre);
			set @anumErr = 1;
		END

END	