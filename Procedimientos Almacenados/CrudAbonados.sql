USE [dbGuarderia]
GO
/****** Object:  StoredProcedure [dbo].[stp_CPABONADOS_ACTUALIZAR]    Script Date: 15/02/2021 11:11:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create or alter  procedure [dbo].[stp_CPABONADOS_CRUD] (

@aID int =-1,
@aIDchildRelation int =-1,
@aDNI varchar(15)='' ,
@aNombre varchar(150)='' ,
@aDireccion varchar(100) ='',
@aTelefono varchar(10)= '',
@aBanco varchar(20)= '',
@aCuentaIBAM varchar(35) = '',
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
			if(	@aIDchildRelation =-1)
					BEGIN
						set @aMensajeError = 'El abonado debe estar asociado a un niño.'
						set @anumErr = 0;
						return 0;
					END
				if(	@aDNI ='')
					BEGIN
						set @aMensajeError = 'El DNI del Abonado no puede ir vacio.'
						set @anumErr = 0;
						return 0;
					END
				if(	@aNombre ='')
					BEGIN
						set @aMensajeError = 'El Nombre del Abonado no puede ir vacio.'
						set @anumErr = 0;
						return 0;
					END
				--if(	@aDireccion ='')
				--	BEGIN
				--		set @aMensajeError = 'La Dirección del Abonado no puede ir vacio.'
				--		set @anumErr = 0;
				--		return 0;
				--	END
				if(	@aTelefono ='')
					BEGIN
						set @aMensajeError = 'El Telefono del Abonado no puede ir vacio.'
						set @anumErr = 0;
						return 0;
					END
				if(	@aBanco ='')
					BEGIN
						set @aMensajeError = 'El Nombre de Banco del Abonado no puede ir vacio.'
						set @anumErr = 0;
						return 0;
					END
				if(	@aCuentaIBAM ='')
					BEGIN
						set @aMensajeError = 'La Cuenta IBAN del Abonado no puede ir vacio.'
						set @anumErr = 0;
						return 0;
					END

				INSERT INTO [dbo].[Abonados]

					   ([IDchildRelation]
					   ,[DNI]
					   ,[Nombre]
					   ,[Direccion]
					   ,[Telefono]
					   ,[Banco]
					   ,[CuentaIBAM])
				 VALUES
					   (@aIDchildRelation
					   ,@aDNI
					   ,@aNombre
					   ,@aDireccion
					   ,@aTelefono
					   ,@aBanco
					   ,@aCuentaIBAM)
				
					set @aMensajeError = 'El Abonado se ha agregado con éxito.'
						set @anumErr = 1;
					return 1
			END

	
		----Borrar un Abonado
		IF (@aOperacion='d')
		BEGIN
		--Hay que eliminar por el ID, EL ID es el Primary key
			if(	@aID =-1)
					BEGIN
						set @aMensajeError = 'El ID del abonado es nulo, 0 registros afectados.'
						set @anumErr = 0;
						return 0;
					END
			
				DELETE FROM [dbo].[Abonados]
					  WHERE ID = @aID     --Se elimina por IDs no por el foreing key 
			
			set @aMensajeError = 'Se Eliminó correctamente.'
			set @anumErr = 1;
		END

		----Actualizar un Abonado
		IF (@aOperacion='u')
		BEGIN
				if(	@aID =-1)
					BEGIN
						set @aMensajeError = 'El ID del abonado es nulo, 0 registros afectados.'
						set @anumErr = 0;
						return 0;
					END
				
				if(	@aDNI ='')
					BEGIN
						set @aMensajeError = 'El DNI del Abonado no puede ir vacio.'
						set @anumErr = 0;
						return 0;
					END
				if(	@aNombre ='')
					BEGIN
						set @aMensajeError = 'El Nombre del Abonado no puede ir vacio.'
						set @anumErr = 0;
						return 0;
					END
				if(	@aTelefono ='')
					BEGIN
						set @aMensajeError = 'El Telefono del Abonado no puede ir vacio.'
						set @anumErr = 0;
						return 0;
					END
				if(	@aBanco ='')
					BEGIN
						set @aMensajeError = 'El Nombre de Banco del Abonado no puede ir vacio.'
						set @anumErr = 0;
						return 0;
					END
				if(	@aCuentaIBAM ='')
					BEGIN
						set @aMensajeError = 'La Cuenta IBAN del Abonado no puede ir vacio.'
						set @anumErr = 0;
						return 0;
					END
				--Cambios, no vamos a cambiar el DNI
				UPDATE [dbo].[Abonados]
					SET 
				   [NOMBRE] = @aNombre
				  ,[Direccion] = @aDireccion
				  ,[Telefono] = @aTelefono
				  ,[Banco] = @aBanco
				  ,[CuentaIBAM] = @aCuentaIBAM
					  WHERE ID = @aID     --Se ACTUALIZA por IDs no por el foreing key 
			  
			set @aMensajeError = 'Se Actualizó correctamente.'
			set @anumErr = 1;
	 		
		END

		-----
		IF (@aOperacion='l')
		BEGIN
				SELECT  
				   [ID]
				  ,[IDchildRelation]
				  ,[DNI]
				  ,[Nombre]
				  ,[Direccion]
				  ,[Telefono]
				  ,[Banco]
				  ,[CuentaIBAM]
				 
			  FROM [dbGuarderia].[dbo].[Abonados]
			  
			set @aMensajeError = 'Se listó correctamente.'
			set @anumErr = 1;
			
		END
		
		IF (@aOperacion='b')
		BEGIN

			SELECT  
				   [ID]
				  ,[IDchildRelation]
				  ,[DNI]
				  ,[Nombre]
				  ,[Direccion]
				  ,[Telefono]
				  ,[Banco]
				  ,[CuentaIBAM]
			  FROM [dbGuarderia].[dbo].[Abonados]
			   WHERE 
			   [IDchildRelation] like '%'+ @aIDchildRelation+'%' and
			   [DNI] like '%'+ @aDNI+'%' and
			   [Nombre] like '%'+ @aNombre+'%' and
			   [Direccion] like '%'+ @aDireccion+'%' and
			   [Telefono] like '%'+ @aTelefono+'%' and
			   [Banco] like '%'+ @aBanco+'%' and
			   [CuentaIBAM] like '%'+ @aCuentaIBAM+'%' 

			   
			set @aMensajeError = 'Se buscó correctamente.'
			set @anumErr = 1;
		END


		IF (@aOperacion='g')
		BEGIN
				if(	@aID =-1)
					BEGIN
						set @aMensajeError = 'El ID del Abonado no puede ir vacio.'
						return 0;
					END
			
			SELECT 			
					ID ----Se necesita el ID en todo momento
				  ,[IDchildRelation]
				  ,[DNI]
				  ,[Nombre]
				  ,[Direccion]
				  ,[Telefono]
				  ,[Banco]
				  ,[CuentaIBAM]
			  FROM [dbGuarderia].[dbo].[Abonados]
					  WHERE ID = @aID     --Se obtiene por IDs no por el foreing key 
			  
			set @aMensajeError = 'Se obtuvo correctamente por el ID' + CONVERT (VARCHAR(MAX) ,@aID);
			set @anumErr = 1;
		END

END	