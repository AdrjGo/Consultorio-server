## Realizar Migraciones

dotnet ef migrations add NOMBRE --project Infrastructure --startup-project Web.API

## Aplicar Migraciones

dotnet ef database update --project Infrastructure --startup-project Web.API

## Diagrama de entidades

```mermaid
erDiagram

    consultorio {
        uuid id PK
        varchar nombre
        text logo_ref
        text url_externo
        varchar direccion
        varchar telefono
        varchar celular
        varchar correo
        uuid encargado FK
    }

    submodulos {
        integer submod_id PK
        uuid form_version_id FK
        varchar nombre
    }

    persona {
        uuid id PK
        varchar nombre
        varchar apellido
        date fecha_nacimiento
        varchar ci
        varchar correo
        varchar celular
        varchar profesion
    }

    formulario_version {
        uuid version_id PK
        uuid form_id FK
        integer numero_version
        jsonb json_schema
        boolean activo
        varchar creado_por
        timestamptz fecha_creacion
        varchar modificado_por
        timestamptz fecha_modificacion
    }

    formulario {
        uuid form_id PK
        varchar nombre_form
        varchar descripcion
    }

    respuestas_form {
        uuid respuestas_id PK
        uuid form_version_id FK
        uuid paciente_id FK
        jsonb respuestas_json
        timestamptz fecha_respuesta
    }

    usuario {
        uuid usuario_id PK
        uuid persona_id FK
        boolean activo
        varchar password
        timestamptz fecha_creacion
        timestamptz fecha_modificacion
    }

    usuario_rol {
        uuid usuario_rol_id PK
        uuid usuario_id FK
        uuid rol_id FK
        timestamptz fecha_asignacion
        timestamptz fecha_revocacion
    }

    rol {
        uuid rol_id PK
        varchar nombre
        varchar descripcion
        date fecha_creacion
        date fecha_modificacion
        varchar creado_por
        varchar modificado_por
    }

    permiso_rol {
        uuid permiso_rol_id PK
        uuid rol_id FK
        uuid permiso_id FK
        date fecha_asignacion
        date fecha_revocacion
    }

    permiso {
        uuid permiso_id PK
        varchar nombre
        varchar descripcion
        date fecha_creacion
        date fecha_revocacion
    }

    cita {
        uuid cita_id PK
        uuid profesional_id FK
        uuid paciente_id FK
        timestamptz inicio
        timestamptz final
        varchar estado
        varchar tipo_cita
        varchar motivo
        varchar observaciones
        varchar creado_por
        timestamptz fecha_creacion
        varchar modificado_por
        timestamptz fecha_modificacion
    }

    seguimiento {
        uuid seguimiento_id PK
        uuid cita_id FK
        varchar nomenclatura
        varchar tratamiento
    }

    archivo_evidencia {
        integer archivo_id PK
        uuid seguimiento_id FK
        varchar tipo
        text referencia
        text url_externo
        varchar descripcion
        timestamptz fecha_carga
        timestamptz fecha_modificacion
    }

    paciente {
        uuid paciente_id PK
        uuid persona_id FK
        varchar direccion
        varchar zona
        varchar cuidad
        varchar telefono
        varchar ocupacion
        varchar lugar_ocupacion
        varchar remitente
        varchar creado_por
        timestamptz fecha_creacion
        varchar modificado_por
        timestamptz fecha_modificacion
    }

    responsable_paciente {
        uuid responsable_id PK
        uuid persona_id FK
        uuid paciente_id FK
        varchar parentesco
        varchar creado_por
        timestamptz fecha_creacion
        varchar modificado_por
        timestamptz fecha_modificacion
    }

    historial_general {
        uuid historial_id PK
        integer submod_id FK
        uuid paciente_id FK
        uuid salud_general_id
        varchar llenado_por
        varchar parentesco
        varchar creado_por
        timestamptz fecha_creacion
        varchar modificado_por
        timestamptz fecha_modificacion
    }

    examen_pretratamiento {
        uuid examen_id PK
        uuid historial_id FK
        timestamptz fecha_examen
        varchar observaciones
        varchar interconsulta
        varchar pieza
        varchar caries
        varchar tratamiento
        integer costo
    }

    avance_tratamiento {
        uuid avance_id PK
        uuid examen_id FK
        date fecha_avance
        integer pago
        integer deuda
    }

    historia_clinica {
        uuid historia_id PK
        uuid paciente_id FK
        integer submod_id FK
        varchar creado_por
        timestamptz fecha_creacion
        varchar modificado_por
        timestamptz fecha_modificacion
    }

    resumen_tratamiento {
        integer resumen_id PK
        uuid paciente_id FK
        integer submod_id FK
        date fecha_resumen
    }

    contrato_ortodoncia {
        uuid contrato_id PK
        integer submod_id FK
        uuid paciente_id FK
        timestamptz fecha_contrato
    }

    responsable_pagos {
        uuid responsable_id PK
        uuid persona_id FK
        uuid contrato_id FK
        varchar parentesco
    }

    %% Relaciones principales
    usuario ||--o{ usuario_rol : "tiene"
    rol ||--o{ usuario_rol : "asigna"
    rol ||--o{ permiso_rol : "incluye"
    permiso ||--o{ permiso_rol : "asocia"
    persona ||--o{ usuario : "es"
    persona ||--o{ paciente : "es"
    persona ||--o{ responsable_paciente : "es"
    paciente ||--o{ responsable_paciente : "tiene"
    paciente ||--o{ cita : "agenda"
    usuario ||--o{ cita : "atiende"
    cita ||--o{ seguimiento : "genera"
    seguimiento ||--o{ archivo_evidencia : "adjunta"
    paciente ||--o{ historia_clinica : "tiene"
    paciente ||--o{ historial_general : "tiene"
    paciente ||--o{ resumen_tratamiento : "recibe"
    paciente ||--o{ contrato_ortodoncia : "firma"
    contrato_ortodoncia ||--o{ responsable_pagos : "garantiza"
    persona ||--o{ responsable_pagos : "es"
    historial_general ||--o{ examen_pretratamiento : "contiene"
    examen_pretratamiento ||--o{ avance_tratamiento : "genera"
    formulario ||--o{ formulario_version : "versiona"
    formulario_version ||--o{ respuestas_form : "responde"
    paciente ||--o{ respuestas_form : "rellena"
    consultorio ||--o{ persona : "encargado"
    submodulos ||--o{ historia_clinica : "usa"
    submodulos ||--o{ historial_general : "usa"
    submodulos ||--o{ resumen_tratamiento : "usa"
    submodulos ||--o{ contrato_ortodoncia : "usa"
    submodulos ||--o{ formulario_version : "usa"

```
