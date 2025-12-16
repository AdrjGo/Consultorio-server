-- ===============================
-- Seed inicial de Admin y SuperUser
-- ===============================

-- Insertar persona
INSERT INTO public.person 
("person_id", "person_name", "person_last_name", "birth_date", "sex", "ci", "email", "phone_number", "profession", "state", "created_at", "created_by")
VALUES 
('13a8cd23-5185-4f42-8335-9eaadfc17fae', 'Admin', 'Super', '2003-01-18', 0, '0000000', 'admin@super.com', '123123123', 'Admin', 'ACTIVE', NOW(), 'Seeds')
ON CONFLICT ("person_id") DO NOTHING;

-- Insertar persona dentista
INSERT INTO public.person 
("person_id", "person_name", "person_last_name", "birth_date", "sex", "ci", "email", "phone_number", "profession", "state", "created_at", "created_by")
VALUES 
('8832dc7d-710f-45da-b147-aaa8b3b2f977', 'Dentista', '1', '1968-06-23', 0, '1111111', 'dentista@email.com', '12411242', 'Odontólogo', 'ACTIVE', NOW(), 'Seeds')
ON CONFLICT ("person_id") DO NOTHING;


-- Insertar usuario asociado
INSERT INTO public.user
("user_id", "person_id", "password", "state", "created_at", "created_by")
VALUES
('c4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '13a8cd23-5185-4f42-8335-9eaadfc17fae',
'$2a$16$JCCmzFrerVGNZLSaL9Cqf.ntKalIpWxtWj2SJISKGP/pgqC1IQrvC', -- Hash bcrypt de "123456"
0, '2025-01-01', 'Seeds')
ON CONFLICT ("user_id") DO NOTHING;

-- Insertar usuario dentista
INSERT INTO public.user
("user_id", "person_id", "password", "state", "created_at", "created_by")
VALUES
('01429580-7e2d-43b5-ae88-93229119c048', '8832dc7d-710f-45da-b147-aaa8b3b2f977',
'$2a$16$JCCmzFrerVGNZLSaL9Cqf.ntKalIpWxtWj2SJISKGP/pgqC1IQrvC', -- Hash bcrypt de "123456"
0, '2025-01-01', 'Seeds')
ON CONFLICT ("user_id") DO NOTHING;


-- Crear rol Admin si no existe
INSERT INTO public.role ("role_id", "role_name", "role_description", "state", "created_at", "created_by")
VALUES ('d4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', 'Admin', 'Rol administrador', 0, NOW(), 'Seeds')
ON CONFLICT ("role_id") DO NOTHING;

-- Asignar rol al usuario
INSERT INTO public.user_rol ("user_rol_id", "user_id", "role_id", "state", "created_at", "created_by")
VALUES ('06bf85b1-08df-45a6-8607-5626a4045d7a', 'c4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', 0, NOW(), 'Seeds')
ON CONFLICT DO NOTHING;

-- Crear rol Dentista si no existe
INSERT INTO public.role ("role_id", "role_name", "role_description", "state", "created_at", "created_by")
VALUES ('a4b1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', 'Dentista', 'Rol dentista', 0, NOW(), 'Seeds')
ON CONFLICT ("role_id") DO NOTHING;

-- Asignar rol al usuario
INSERT INTO public.user_rol ("user_rol_id", "user_id", "role_id", "state", "created_at", "created_by")
VALUES ('53bf85b1-08df-45a6-8607-5626a4045d7a', 'c4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', 'a4b1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', 0, NOW(), 'Seeds')
ON CONFLICT DO NOTHING;

INSERT INTO public.user_rol ("user_rol_id", "user_id", "role_id", "state", "created_at", "created_by")
VALUES ('54bf85b1-08df-45a6-8607-5626a4045d7a', '01429580-7e2d-43b5-ae88-93229119c048', 'a4b1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', 0, NOW(), 'Seeds')
ON CONFLICT DO NOTHING;


-- ===============================
-- Insertar pacientes
-- ===============================

INSERT INTO public.person 
("person_id", "person_name", "person_last_name", "birth_date", "sex", "ci", "email", "phone_number", "profession", "state", "created_at", "created_by")
VALUES 
('8c0c77a5-3ca1-482c-b078-750a9b82ad5e', 'Paciente', '1', '2000-01-01', 0, '2222222', 'paciente@email.com', '987878678', 'Estudiante', 0, NOW(), 'Seeds')
ON CONFLICT ("person_id") DO NOTHING;

INSERT INTO public.patient
("patient_id", "person_id", "responsible_id", "address", "zone", "city", "home_phone", "occupation", "place_occupation","nit", "sender","state", "created_by", "created_at")
VALUES
('aca377cb-7c24-41b1-acf5-6586093961c0', '8c0c77a5-3ca1-482c-b078-750a9b82ad5e', null, 'Calle 1', 'Zona 1', 'Ciudad 1', '12345678', 'Estudiante', 'Universidad', '000001020', '', 'ACTIVE', 'Seeds', NOW())
ON CONFLICT (patient_id) DO NOTHING;


-- ===============================
-- Insertar cita
-- ===============================
INSERT INTO public.appointment
("appointment_id", "patient_id", "professional_id", "start_date", "end_date", "appointment_type", "status", "reason", "observations", "state", "created_by", "created_at")
VALUES
('eca377cb-7c24-41b1-acf5-6586093961c0', 'aca377cb-7c24-41b1-acf5-6586093961c0', '01429580-7e2d-43b5-ae88-93229119c048', '2025-10-29T12:25:00', '2025-10-29T12:50:00', 'Emergencia', 'Confirmado', 'Ejemplo de razón', 'Ejemplo de observación...', 0, 'Seeds', NOW());


-- ===============================
-- Crear Submodulos
-- ===============================
INSERT INTO public.submodule
("submodule_id", "submodule_name", "state", "created_at", "created_by")
VALUES
('1', 'Historia de Salud General', 'ACTIVE', NOW(), 'Seeds'),
('2', 'Historia Clínica', 'ACTIVE', NOW(), 'Seeds'),
('3', 'Resumen de Tratamiento', 'ACTIVE', NOW(), 'Seeds'),
('4', 'Presupuesto Ortodoncia', 'ACTIVE', NOW(), 'Seeds'),
('5', 'Presupuesto Ortopedia', 'ACTIVE', NOW(), 'Seeds'),
('6', 'Control de Cuotas', 'ACTIVE', NOW(), 'Seeds'),
('7', 'Presupuesto Dental', 'ACTIVE', NOW(), 'Seeds');

-- ===============================
-- Insertar permisos
-- ===============================
INSERT INTO public.permission ("permission_id", "permission_key", "permission_name", "permission_description", "state", "created_at", "created_by")
VALUES
    --User
    ('11111111-1111-1111-1111-111111111111', 'Administración', 'Crear Usuario', 'Permite crear usuarios', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111112', 'Administración', 'Leer Usuario', 'Permite leer usuarios', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111113', 'Administración', 'Actualizar Usuario', 'Permite actualizar usuarios', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111114', 'Administración', 'Eliminar Usuario', 'Permite eliminar usuarios', 0, NOW(), 'Seeds'),

    --UserRole (Asignar Roles)
    ('11111111-1111-1111-1111-111111111115', 'Administración',  'Asignar Roles', 'Permite asignar roles a un usuario', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111116', 'Administración',  'Leer Roles', 'Permite leer roles de usuario', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111117', 'Administración',  'Actualizar Roles', 'Permite actualizar roles de usuario', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111118', 'Administración',  'Eliminar Roles', 'Permite eliminar roles de usuario', 0, NOW(), 'Seeds'),

    --Role
    ('11111111-1111-1111-1111-111111111119', 'Administración', 'Crear Rol', 'Permite crear roles', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111120', 'Administración', 'Leer Rol', 'Permite leer roles', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111121', 'Administración', 'Actualizar Rol', 'Permite actualizar roles', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111122', 'Administración', 'Eliminar Rol', 'Permite eliminar roles', 0, NOW(), 'Seeds'),

    --Permission
    -- ('11111111-1111-1111-1111-111111111123', 'Administración', 'Crear Permiso', 'Permite crear permisos', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111124', 'Administración', 'Leer Permiso', 'Permite leer permisos', 0, NOW(), 'Seeds'),
    -- ('11111111-1111-1111-1111-111111111125', 'Administración', 'Actualizar Permiso', 'Permite actualizar permisos', 0, NOW(), 'Seeds'),
    -- ('11111111-1111-1111-1111-111111111126', 'Administración', 'Eliminar Permiso', 'Permite eliminar permisos', 0, NOW(), 'Seeds'),

    --RolePermission
    ('11111111-1111-1111-1111-111111111127', 'Administración', 'Asignar Permiso a Rol', 'Permite asignar permisos a los roles', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111128', 'Administración', 'Leer Permiso de Rol', 'Permite leer permisos de roles', 0, NOW(), 'Seeds'),
    -- ('11111111-1111-1111-1111-111111111129', 'Actualizar Permiso de Rol', 'Permite actualizar permisos de roles', 0, NOW(), 'Seeds'),
    -- ('11111111-1111-1111-1111-111111111130', 'Eliminar Permiso de Rol', 'Permite eliminar permisos de roles', 0, NOW(), 'Seeds'),

    --Consultorio
    ('11111111-1111-1111-1111-111111111131', 'Consultorio', 'Asignar Datos de Consultorio', 'Permite asigna datos al consultorio', 0, NOW(), 'Seeds'),
    -- ('11111111-1111-1111-1111-111111111132', 'Read.Clinic', 'Permite leer clinicas', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111133', 'Consultorio', 'Actualizar Datos de Consultorio', 'Permite actualizar datos del consultorio', 0, NOW(), 'Seeds'),
    -- ('11111111-1111-1111-1111-111111111134', 'Eliminar Datos de Consultorio', 'Permite eliminar datos del consultorio', 0, NOW(), 'Seeds'),

    --Paciente
    ('11111111-1111-1111-1111-111111111135', 'Pacientes', 'Crear Paciente', 'Permite crear pacientes', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111136', 'Pacientes', 'Leer Paciente', 'Permite leer pacientes', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111137', 'Pacientes', 'Actualizar Paciente', 'Permite actualizar pacientes', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111138', 'Pacientes', 'Eliminar Paciente', 'Permite eliminar pacientes', 0, NOW(), 'Seeds'),

    --Cita
    ('11111111-1111-1111-1111-111111111139', 'Citas', 'Crear Cita', 'Permite crear citas', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111140', 'Citas', 'Leer Cita', 'Permite leer citas', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111141',  'Citas', 'Actualizar Cita', 'Permite actualizar citas', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111142', 'Citas', 'Eliminar Cita', 'Permite eliminar citas', 0, NOW(), 'Seeds'),

    --Archivo de Evidencia
    ('11111111-1111-1111-1111-111111111143', 'Evidencia', 'Asignar Archivo de Evidencia', 'Permite asignar archivos de evidencia', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111144', 'Evidencia', 'Leer Archivo de Evidencia', 'Permite leer archivos de evidencia', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111145', 'Evidencia', 'Actualizar Archivo de Evidencia', 'Permite actualizar archivos de evidencia', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111146', 'Evidencia', 'Eliminar Archivo de Evidencia', 'Permite eliminar archivos de evidencia', 0, NOW(), 'Seeds'),

    --Monitoring (Seguimiento)
    ('11111111-1111-1111-1111-111111111147', 'Seguimiento de citas', 'Añadir Seguimiento', 'Permite añadir seguimientos a las citas', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111148', 'Seguimiento de citas', 'Leer Seguimiento', 'Permite leer seguimientos de citas', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111149', 'Seguimiento de citas', 'Actualizar Seguimiento', 'Permite actualizar seguimientos de citas', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111150', 'Seguimiento de citas', 'Eliminar Seguimiento', 'Permite eliminar seguimientos de citas', 0, NOW(), 'Seeds'),

    --DynamicForm
    ('11111111-1111-1111-1111-111111111151', 'Configuración', 'Crear Formularios Dinámicos', 'Permite crear formularios dinámicos', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111152', 'Configuración', 'Leer Formularios Dinámicos', 'Permite leer formularios dinámicos', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111153', 'Configuración', 'Actualizar Formularios Dinámicos', 'Permite actualizar formularios dinámicos', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111154', 'Configuración', 'Eliminar Formularios Dinámicos', 'Permite eliminar formularios dinámicos', 0, NOW(), 'Seeds'),

    --Submodule
    ('11111111-1111-1111-1111-111111111155', 'Configuración', 'Crear Submódulos', 'Permite crear submódulos', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111156', 'Configuración', 'Leer Submódulos', 'Permite leer submódulos', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111157', 'Configuración', 'Actualizar Submódulos', 'Permite actualizar submódulos', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111158', 'Configuración', 'Eliminar Submódulos', 'Permite eliminar submódulos', 0, NOW(), 'Seeds'),

    --Pretreatment Exam
    ('11111111-1111-1111-1111-111111111159', 'Paciente', 'Crear Examen de tratamiento', 'Permite crear examen de tratamiento', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111160', 'Paciente', 'Leer Examen de tratamiento', 'Permite leer examen de tratamiento', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111161', 'Paciente', 'Actualizar Examen de tratamiento', 'Permite actualizar examen de tratamiento', 0, NOW(), 'Seeds'),
    -- ('11111111-1111-1111-1111-111111111162', 'Paciente', 'Eliminar Examen de tratamiento', 'Permite eliminar examen de tratamiento', 0, NOW(), 'Seeds');

    -- Treatment Progress
    ('11111111-1111-1111-1111-111111111163', 'Paciente', 'Crear Avance de tratamiento', 'Permite crear avance de tratamiento (pago)', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111164', 'Paciente', 'Leer Avance de tratamiento', 'Permite leer avance de tratamiento (pago)', 0, NOW(), 'Seeds'),
    -- ('11111111-1111-1111-1111-111111111165', 'Paciente', 'Actualizar Avance de tratamiento', 'Permite actualizar avance de tratamiento', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111166', 'Paciente', 'Eliminar Avance de tratamiento', 'Permite eliminar avance de tratamiento (pago)', 0, NOW(), 'Seeds');

-- ===============================
-- Asignar todos los permisos al rol Admin
-- ===============================
INSERT INTO public.role_permission ("role_permission_id", "role_id", "permission_id", "state", "created_at", "created_by")
VALUES
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111111', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111112', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111113', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111114', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111115', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111116', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111117', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111118', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111119', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111120', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111121', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111122', 0, NOW(), 'Seeds'),
    -- (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111123', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111124', 0, NOW(), 'Seeds'),
    -- (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111125', 0, NOW(), 'Seeds'),
    -- (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111126', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111127', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111128', 0, NOW(), 'Seeds'),
    -- (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111129', 0, NOW(), 'Seeds'),
    -- (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111130', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111131', 0, NOW(), 'Seeds'),
    -- (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111132', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111133', 0, NOW(), 'Seeds'),
    -- (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111134', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111135', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111136', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111137', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111138', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111139', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111140', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111141', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111142', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111143', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111144', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111145', 0, NOW(), 'Seeds'), 
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111146', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111147', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111148', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111149', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111150', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111151', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111152', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111153', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111154', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111155', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111156', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111157', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111158', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111159', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111160', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111161', 0, NOW(), 'Seeds'),
    -- (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111162', 0, NOW(), 'Seeds');
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111163', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111164', 0, NOW(), 'Seeds'),
    -- (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111165', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111166', 0, NOW(), 'Seeds');



-- ===============================
-- Asignar algunos permisos al rol Dentista
-- ===============================

INSERT INTO public.role_permission ("role_permission_id", "role_id", "permission_id", "state", "created_at", "created_by")
VALUES
    (gen_random_uuid(), 'a4b1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111111', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'a4b1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111112', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'a4b1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111113', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'a4b1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111114', 0, NOW(), 'Seeds');