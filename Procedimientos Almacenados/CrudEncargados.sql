SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create or alter  procedure [dbo].[stp_CPEncargados_CRUD] (

@aID int =-1,
@aIDchildRelation int =-1,
@aDNI varchar(15)='' ,
@aNombre varchar(150)='',
@aDireccion varchar (150)='',
@aTelefono varchar (10)='',
@pOperacion varchar(2) = 'n',
@pMensajeError varchar(max) = 'no definido' output  ,
@pnumErr int= -1 output  
)
as 
BEGIN

		if(@pOperacion = 'n')
			BEGIN
				return 0;
			END
				----Insertar una Relación
		IF (@pOperacion='i')
			BEGIN
			if(	@aIDchildRelation=-1)
					BEGIN
						set @pMensajeError = 'El Encargado no tiene relación  con el niño.'
						set @pnumErr = 0;
						return 0;
					END
				
				if(	@aDNI ='')
					BEGIN
						set @pMensajeError = ' No puede ir vacio.'
						set @pnumErr = 0;
						return 0;
					END

				if(	@aNombre ='')
					BEGIN
						set @pMensajeError = 'El Nombre del Encargado no puede ir vacio.'
						set @pnumErr = 0;
						return 0;
					END

				if(	@aDireccion ='')
					BEGIN
						set @pMensajeError = 'La Dirección del Encargado no puede ir vacio.'
						set @pnumErr = 0;
						return 0;
					END

				if(	@aTelefono ='')
					BEGIN
						set @pMensajeError = 'El Télefono del Encargado no puede ir vacio.'
						set @pnumErr = 0;
						return 0;
					END

					INSERT INTO [dbo].[Encargados]
						 ([IDchildRelation],
						 [DNI]
						 ,[Nombre],
						 [Direccion]
					    ,[Telefono])
   
				 VALUES
					   (@aIDchildRelation,
					    @aDNI,
						@aNombre,
						@aDireccion,
					   @aTelefono)
				
					set @pMensajeError = 'El Encargado se ha agregado con éxito.'
						set @pnumErr = 1;
					return 1
			END

	
		----Borrar un Relación
		IF (@pOperacion='d')
		BEGIN
		--Hay que eliminar por el ID, EL ID es el Primary key
			if(	@aID =-1)
					BEGIN
						set @pMensajeError = 'El ID del Encargado es nulo, 0 registros afectados.'
						set @pnumErr = 0;
						return 0;
					END
			
				DELETE FROM [dbo].[Encargados]
					  WHERE ID = @aID     --Se elimina por IDs no por el foreing key 
			
			set @pMensajeError = 'Se Eliminó correctamente.'
			set @pnumErr = 1;
		END

		----Actualizar una Relación
		IF (@pOperacion='u')
		BEGIN
				if(	@aIDchildRelation=-1)
					BEGIN
						set @pMensajeError = 'El Encargado no tiene relación  con el niño.'
						set @pnumErr = 0;
						return 0;
					END
				
				if(	@aDNI ='')
					BEGIN
						set @pMensajeError = ' No puede ir vacio.'
						set @pnumErr = 0;
						return 0;
					END

				if(	@aNombre ='')
					BEGIN
						set @pMensajeError = 'El Nombre del Encargado no puede ir vacio.'
						set @pnumErr = 0;
						return 0;
					END

				if(	@aDireccion ='')
					BEGIN
						set @pMensajeError = 'La Dirección del Encargado no puede ir vacio.'
						set @pnumErr = 0;
						return 0;
					END

				if(	@aTelefono ='')
					BEGIN
						set @pMensajeError = 'El Télefono del Encargado no puede ir vacio.'
						set @pnumErr = 0;
						return 0;
					END
				
				--Cambios, no vamos a cambiar el DNI
				UPDATE [dbo].[Encargados]
					SET 
				   [IDchildRelation] = @aIDchildRelation
				  ,[Nombre] = @aNombre,
				  [Direccion] = @aDireccion,
				  [Telefono] = @aTelefono
					 
					WHERE ID = @aID     --Se ACTUALIZA por IDs no por el foreing key 
			  
			set @pMensajeError = 'Se Actualizó correctamente.'
			set @pnumErr = 1;
	 		
		END

		-----
		IF (@pOperacion='l')
		BEGIN
				SELECT  e.[ID],
				   [IDchildRelation],
				   e.DNI,
				   [Nombre],
				   [Direccion],
				   [Telefono]
				  			 
			  FROM [dbo].[Encargados] as e inner join dbo.childsRelations as c on e.IDchildRelation=c.ID
			  
			  
			set @pMensajeError = 'Se listó correctamente.'
			set @pnumErr = 1;
			
		END
				IF (@pOperacion='lb')
		BEGIN
				SELECT   e.[ID],
				   [IDchildRelation],
				   e.DNI,
				   [Nombre],
				   [Direccion],
				   [Telefono]
			  FROM [dbo].[Encargados] as e 
			  inner join dbo.childsRelations as c on e.IDchildRelation=c.ID
			  where e.DNI=@aDNI
			  
			set @pMensajeError = 'Se listó correctamente.'
			set @pnumErr = 1;
			
		END
		IF (@pOperacion='b')
		BEGIN
			SELECT  
				   [IDchildRelation],
				   [Nombre],
				   [Direccion],
				   [Telefono]
				  			 
			  FROM @pnumErr[dbo].[Encargados]  as e inner join dbo.childsRelations as c on e.IDchildRelation=c.ID
			  where e.IDchildRelation=c.ID and
			  
			  [IDchildRelation] like '%'+ @aIDchildRelation+'%' and
			   [Nombre] like '%'+ @aNombre+'%' and  [Direccion] like '%'+ @aDireccion+'%' 
			   and  [Telefono] like '%'+ @aTelefono+'%'

			   
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
				   [IDchildRelation],
				   [Nombre],
				   [Direccion],
				   [Telefono]
				  			 
			  FROM @pnumErr[dbo].[Encargados] 
			  where  ID = @aID     --Se obtiene por IDs no por el foreing key 			  
			set @pMensajeError = 'Se obtuvo correctamente por el ID' + CONVERT (VARCHAR(MAX) ,@aID);
			set @pnumErr = 1;
		END
END
