namespace Ineval.DAL.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class _010920212359 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DatosTemporales",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        usu_id = c.String(),
                        tipo_identificacion = c.String(),
                        identificacion = c.String(),
                        primer_nombre = c.String(),
                        segundo_nombre = c.String(),
                        primer_apellido = c.String(),
                        segundo_apellido = c.String(),
                        sexo = c.String(),
                        grado = c.String(),
                        paralelo = c.String(),
                        dia_nacimiento = c.String(),
                        mes_nacimiento = c.String(),
                        anio_nacimiento = c.String(),
                        pais_nacimiento = c.String(),
                        provincia_nacimiento = c.String(),
                        discapacidad = c.String(),
                        tipo_discapacidad = c.String(),
                        porcentaje_discapacidad = c.String(),
                        correo_sustentante = c.String(),
                        telefono_sustentante = c.String(),
                        telefono_sustentante_secundario = c.String(),
                        celular_sustentante = c.String(),
                        jornada_sustentante = c.String(),
                        saber = c.String(),
                        amie = c.String(),
                        nombre_institucion = c.String(),
                        id_provincia = c.String(),
                        provincia = c.String(),
                        canton_id = c.String(),
                        canton = c.String(),
                        id_parroquia = c.String(),
                        parroquia = c.String(),
                        id_circuito = c.String(),
                        circuito = c.String(),
                        id_distrito = c.String(),
                        distrito = c.String(),
                        id_zona = c.String(),
                        sostenimiento_institucion = c.String(),
                        regimen_institucion = c.String(),
                        ciclo = c.String(),
                        poblacion = c.String(),
                        modalidad = c.String(),
                        coordenada_x = c.String(),
                        coordenada_y = c.String(),
                        computador = c.String(),
                        internet = c.String(),
                        conexion_internet = c.String(),
                        camara_web = c.String(),
                        microfono = c.String(),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaModificacion = c.DateTime(),
                        FechaEliminacion = c.DateTime(),
                        Estado = c.Byte(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DatosTemporales_RegistrosEliminados", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DatosTemporales",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DatosTemporales_RegistrosEliminados", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
