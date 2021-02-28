
/****** Object:  StoredProcedure [dbo].[stp_CPABONADOS_ACTUALIZAR]    Script Date: 15/02/2021 11:11:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create or alter  procedure [dbo].[stp_CPchildsRelations_CRUD] (

@aID int =-1,
@aIDchild int =-1,
@aTipoRelacion varchar(15)='' ,
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
			if(	@aIDchild=-1)
					BEGIN
						set @aMensajeError = 'No existe relación con el niño.'
						set @anumErr = 0;
						return 0;
					END
				
				if(	@aTipoRelacion ='')
					BEGIN
						set @aMensajeError = 'El tipo de Relación no puede ir vacio.'
						set @anumErr = 0;
						return 0;
					END
				

					INSERT INTO [dbo].[childsRelations]
						 ([IDchild]
					    ,[TipoRelacion])
   
				 VALUES
					   (@aIDchild
					   ,@aTipoRelacion)
				
					set @aMensajeError = 'La Relación se ha agregado con éxito.'
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
			
				DELETE FROM [dbo].[Encargados]
					  WHERE ID = @aID     --Se elimina por IDs no por el foreing key 
			
			set @aMensajeError = 'Se Eliminó correctamente.'
			set @anumErr = 1;
		END

		----Actualizar una Relación
		IF (@aOperacion='u')
		BEGIN
				if(	@aIDchild=-1)
					BEGIN
						set @aMensajeError = 'No existe relación con el niño.'
						set @anumErr = 0;
						return 0;
					END
				
				if(	@aTipoRelacion ='')
					BEGIN
						set @aMensajeError = 'El tipo de Relación no puede ir vacio.'
						set @anumErr = 0;
						return 0;
					END
				
				--Cambios, no vamos a cambiar el DNI
				UPDATE [dbo].[childsRelations]
					SET 
				   [IDchild] = @aIDchild
				  ,[TipoRelacion] = @aTipoRelacion
					 
					WHERE ID = @aID     --Se ACTUALIZA por IDs no por el foreing key 
			  
			set @aMensajeError = 'Se Actualizó correctamente.'
			set @anumErr = 1;
	 		
		END

		-----
		IF (@aOperacion='l')
		BEGIN
				SELECT  
				   [IDchild]
				  ,[TipoRelacion]
				  			 
			  FROM [dbGuarderia].[dbo].[childsRelations] as c inner join dbo.Childs as ci on c.IDchild=ci.IDmatricula
			  where c.IDchild=ci.IDmatricula
			  
			set @aMensajeError = 'Se listó correctamente.'
			set @anumErr = 1;
			
		END
		
		IF (@aOperacion='b')
		BEGIN

			SELECT  
				  [IDchild],
				  [TipoRelacion]

			  FROM [dbGuarderia].[dbo].[childsRelations]  as c inner join dbo.Childs as ci on c.IDchild= ci.IDmatricula
			  where c.IDchild= ci.IDmatricula and
			   [IDchild] like '%'+ @aIDchild+'%' and
			   [TipoRelacion] like '%'+ @aTipoRelacion+'%'

			   
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
					ID ----Se necesita el ID en todo momento
				  ,[IDchild]
				  ,[TipoRelacion]

			  FROM [dbGuarderia].[dbo].[childsRelations] 
			  where  ID = @aID     --Se obtiene por IDs no por el foreing key 
			  
			set @aMensajeError = 'Se obtuvo correctamente por el ID' + CONVERT (VARCHAR(MAX) ,@aID);
			set @anumErr = 1;
		END
END
