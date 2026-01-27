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
('8c0c77a5-3ca1-482c-b078-750a9b82ad5e', 'Paciente', '1', '2000-01-01', 0, '2222222', 'paciente@email.com', '987878678', 'Estudiante', 0, NOW(), 'Seeds'),
('9a1a77a5-0001-0001-b078-750a9b82aaa1', 'Carlos Andrés', 'García López', '1990-05-10', 1, '2222223', 'carlos.garcia@email.com', '700000001', 'Docente', 0, NOW(), 'Seeds'),
('9a1a77a5-0002-0002-b078-750a9b82aaa2', 'María Fernanda', 'Ramírez Pérez', '1985-03-15', 0, '2222224', 'maria.ramirez@email.com', '700000002', 'Abogada', 0, NOW(), 'Seeds'),
('9a1a77a5-0003-0003-b078-750a9b82aaa3', 'Luis Alberto', 'Sánchez Morales', '1992-11-23', 1, '2222225', 'luis.sanchez@email.com', '700000003', 'Ingeniero Civil', 0, NOW(), 'Seeds'),
('9a1a77a5-0004-0004-b078-750a9b82aaa4', 'Ana Paula', 'Vargas Medina', '1998-08-19', 0, '2222226', 'ana.vargas@email.com', '700000004', 'Diseñadora Gráfica', 0, NOW(), 'Seeds'),
('9a1a77a5-0005-0005-b078-750a9b82aaa5', 'Diego', 'Torres Ríos', '2001-02-28', 1, '2222227', 'diego.torres@email.com', '700000005', 'Estudiante', 0, NOW(), 'Seeds'),
('9a1a77a5-0006-0006-b078-750a9b82aaa6', 'Valentina', 'Castillo Herrera', '1995-06-30', 0, '2222228', 'valentina.castillo@email.com', '700000006', 'Técnica de Laboratorio', 0, NOW(), 'Seeds'),
('9a1a77a5-0007-0007-b078-750a9b82aaa7', 'Jorge Luis', 'Mendoza Ortega', '1988-09-17', 1, '2222229', 'jorge.mendoza@email.com', '700000007', 'Comerciante', 0, NOW(), 'Seeds'),
('9a1a77a5-0008-0008-b078-750a9b82aaa8', 'Isabel', 'Cruz Delgado', '1994-12-05', 0, '2222230', 'isabel.cruz@email.com', '700000008', 'Chef', 0, NOW(), 'Seeds'),
('9a1a77a5-0009-0009-b078-750a9b82aaa9', 'Sergio', 'Paredes Zamora', '1987-07-12', 1, '2222231', 'sergio.paredes@email.com', '700000009', 'Contador Público', 0, NOW(), 'Seeds'),
('9a1a77a5-0010-0010-b078-750a9b82aa10', 'Daniela', 'López Salazar', '2003-04-08', 0, '2222232', 'daniela.lopez@email.com', '700000010', 'Estudiante', 0, NOW(), 'Seeds')
ON CONFLICT ("person_id") DO NOTHING;


INSERT INTO public.patient
("patient_id", "person_id", "responsible_id", "address", "zone", "city", "home_phone", "occupation", "place_occupation","nit", "sender","state", "created_by", "created_at")
VALUES
('aca377cb-7c24-41b1-acf5-6586093961c0', '8c0c77a5-3ca1-482c-b078-750a9b82ad5e', null, 'Calle 1', 'Zona 1', 'Ciudad 1', '12345678', 'Estudiante', 'Universidad', '000001020', '', 'ACTIVE', 'Seeds', NOW()),
('aca377cb-0001-0001-acf5-658609390001', '9a1a77a5-0001-0001-b078-750a9b82aaa1', null, 'Calle A', 'Zona 1', 'Santa Cruz', '800000001', 'Docente', 'Colegio Central', '000001021', '', 'ACTIVE', 'Seeds', NOW()),
('aca377cb-0002-0002-acf5-658609390002', '9a1a77a5-0002-0002-b078-750a9b82aaa2', null, 'Calle B', 'Zona 2', 'La Paz', '800000002', 'Abogada', 'Estudio Legal', '000001022', '', 'ACTIVE', 'Seeds', NOW()),
('aca377cb-0003-0003-acf5-658609390003', '9a1a77a5-0003-0003-b078-750a9b82aaa3', null, 'Calle C', 'Zona 3', 'Cochabamba', '800000003', 'Ingeniero Civil', 'Constructora ABC', '000001023', '', 'ACTIVE', 'Seeds', NOW()),
('aca377cb-0004-0004-acf5-658609390004', '9a1a77a5-0004-0004-b078-750a9b82aaa4', null, 'Calle D', 'Zona 4', 'Tarija', '800000004', 'Diseñadora Gráfica', 'Agencia Creativa', '000001024', '', 'ACTIVE', 'Seeds', NOW()),
('aca377cb-0005-0005-acf5-658609390005', '9a1a77a5-0005-0005-b078-750a9b82aaa5', null, 'Calle E', 'Zona 5', 'Oruro', '800000005', 'Estudiante', 'Universidad Técnica', '000001025', '', 'ACTIVE', 'Seeds', NOW()),
('aca377cb-0006-0006-acf5-658609390006', '9a1a77a5-0006-0006-b078-750a9b82aaa6', null, 'Calle F', 'Zona 6', 'Sucre', '800000006', 'Técnica de Laboratorio', 'Hospital Regional', '000001026', '', 'ACTIVE', 'Seeds', NOW()),
('aca377cb-0007-0007-acf5-658609390007', '9a1a77a5-0007-0007-b078-750a9b82aaa7', null, 'Calle G', 'Zona 7', 'Potosí', '800000007', 'Comerciante', 'Mercado Popular', '000001027', '', 'ACTIVE', 'Seeds', NOW()),
('aca377cb-0008-0008-acf5-658609390008', '9a1a77a5-0008-0008-b078-750a9b82aaa8', null, 'Calle H', 'Zona 8', 'Trinidad', '800000008', 'Chef', 'Restaurante El Sabor', '000001028', '', 'ACTIVE', 'Seeds', NOW()),
('aca377cb-0009-0009-acf5-658609390009', '9a1a77a5-0009-0009-b078-750a9b82aaa9', null, 'Calle I', 'Zona 9', 'Cobija', '800000009', 'Contador Público', 'Firma Contable ABC', '000001029', '', 'ACTIVE', 'Seeds', NOW()),
('aca377cb-0010-0010-acf5-658609390010', '9a1a77a5-0010-0010-b078-750a9b82aa10', null, 'Calle J', 'Zona 10', 'Tarija', '800000010', 'Estudiante', 'Colegio Nacional', '000001030', '', 'ACTIVE', 'Seeds', NOW())
ON CONFLICT (patient_id) DO NOTHING;


-- ===============================
-- Insertar citas diciembre 2025 - febrero 2026
-- ===============================

INSERT INTO public.appointment
("appointment_id", "patient_id", "professional_id", "start_date", "end_date", "appointment_type", "status", "life_status", "reason", "observations", "state", "created_by", "created_at", "start_by", "start_at")
VALUES
-- Diciembre 2025
(gen_random_uuid(), 'aca377cb-7c24-41b1-acf5-6586093961c0', '01429580-7e2d-43b5-ae88-93229119c048', '2025-12-01T09:00:00', '2025-12-01T09:30:00', 'Consulta', 'Confirmado', 'Completada', 'Chequeo general', 'Sin observaciones', 0, 'Seeds', NOW(), NULL, NULL),
(gen_random_uuid(), 'aca377cb-0001-0001-acf5-658609390001', '01429580-7e2d-43b5-ae88-93229119c048', '2025-12-02T10:00:00', '2025-12-02T10:30:00', 'Emergencia', 'Pendiente', 'NoIniciado', 'Dolor agudo', '', 0, 'Seeds', NOW(), NULL, NULL),
(gen_random_uuid(), 'aca377cb-0002-0002-acf5-658609390002', '01429580-7e2d-43b5-ae88-93229119c048', '2025-12-03T11:00:00', '2025-12-03T11:30:00', 'Tratamiento', 'Programado', 'NoIniciado', 'Inicio de tratamiento de ortodoncia', '', 0, 'Seeds', NOW(), NULL, NULL),
(gen_random_uuid(), 'aca377cb-0003-0003-acf5-658609390003', '01429580-7e2d-43b5-ae88-93229119c048', '2025-12-04T09:00:00', '2025-12-04T09:30:00', 'Reconsulta', 'Confirmado', 'Completada', 'Seguimiento de limpieza', '', 0, 'Seeds', NOW(), NULL, NULL),
(gen_random_uuid(), 'aca377cb-0004-0004-acf5-658609390004', '01429580-7e2d-43b5-ae88-93229119c048', '2025-12-04T10:00:00', '2025-12-04T10:30:00', 'Seguimiento', 'Reprogramado', 'NoIniciado', 'Evaluar progreso', '', 0, 'Seeds', NOW(), NULL, NULL),
(gen_random_uuid(), 'aca377cb-0005-0005-acf5-658609390005', '01429580-7e2d-43b5-ae88-93229119c048', '2025-12-05T09:00:00', '2025-12-05T09:45:00', 'Emergencia', 'Cancelado', 'NoIniciado', 'Falta de tiempo del paciente', '', 0, 'Seeds', NOW(), NULL, NULL),
-- Enero 2026
(gen_random_uuid(), 'aca377cb-0006-0006-acf5-658609390006', '01429580-7e2d-43b5-ae88-93229119c048', '2026-01-08T09:30:00', '2026-01-08T10:00:00', 'Limpieza', 'Programado', 'NoIniciado', 'Limpieza semestral', '', 0, 'Seeds', NOW(), NULL, NULL),
(gen_random_uuid(), 'aca377cb-0007-0007-acf5-658609390007', '01429580-7e2d-43b5-ae88-93229119c048', '2026-01-09T11:00:00', '2026-01-09T11:45:00', 'Consulta', 'Confirmado', 'EnCurso', 'Consulta general', '', 0, 'Seeds', NOW(), NULL, NULL),
(gen_random_uuid(), 'aca377cb-0008-0008-acf5-658609390008', '01429580-7e2d-43b5-ae88-93229119c048', '2026-01-10T08:30:00', '2026-01-10T09:00:00', 'Revision_de_progresos', 'Pendiente', 'NoIniciado', 'Evaluación mensual', '', 0, 'Seeds', NOW(), NULL, NULL),
(gen_random_uuid(), 'aca377cb-0009-0009-acf5-658609390009', '01429580-7e2d-43b5-ae88-93229119c048', '2026-01-11T10:15:00', '2026-01-11T10:45:00', 'Tratamiento', 'Programado', 'NoIniciado', 'Aplicación de resina', '', 0, 'Seeds', NOW(), NULL, NULL),
(gen_random_uuid(), 'aca377cb-0010-0010-acf5-658609390010', '01429580-7e2d-43b5-ae88-93229119c048', '2026-01-12T09:00:00', '2026-01-12T09:30:00', 'Emergencia', 'Confirmado', 'EnCurso', 'Dolor post-tratamiento', '', 0, 'Seeds', NOW(), NULL, NULL),
(gen_random_uuid(), 'aca377cb-0001-0001-acf5-658609390001', '01429580-7e2d-43b5-ae88-93229119c048', '2026-01-13T11:00:00', '2026-01-13T11:30:00', 'Eliminacion_de_aparatos', 'Confirmado', 'Completada', 'Retiro de brackets', '', 0, 'Seeds', NOW(), NULL, NULL),
(gen_random_uuid(), 'aca377cb-0002-0002-acf5-658609390002', '01429580-7e2d-43b5-ae88-93229119c048', '2026-01-14T09:00:00', '2026-01-14T09:45:00', 'Tratamiento', 'Reprogramado', 'NoIniciado', 'Colocación de coronas', '', 0, 'Seeds', NOW(), NULL, NULL),
-- Febrero 2026
(gen_random_uuid(), 'aca377cb-0003-0003-acf5-658609390003', '01429580-7e2d-43b5-ae88-93229119c048', '2026-02-01T10:00:00', '2026-02-01T10:30:00', 'Consulta', 'Programado', 'NoIniciado', 'Consulta por revisión', '', 0, 'Seeds', NOW(), NULL, NULL),
(gen_random_uuid(), 'aca377cb-0004-0004-acf5-658609390004', '01429580-7e2d-43b5-ae88-93229119c048', '2026-02-02T09:00:00', '2026-02-02T09:30:00', 'Seguimiento', 'Confirmado', 'EnCurso', 'Evaluar sensibilidad', '', 0, 'Seeds', NOW(), NULL, NULL),
(gen_random_uuid(), 'aca377cb-0005-0005-acf5-658609390005', '01429580-7e2d-43b5-ae88-93229119c048', '2026-02-03T11:00:00', '2026-02-03T11:30:00', 'Emergencia', 'Pendiente', 'NoIniciado', 'Inflamación dental', '', 0, 'Seeds', NOW(), NULL, NULL),
(gen_random_uuid(), 'aca377cb-0006-0006-acf5-658609390006', '01429580-7e2d-43b5-ae88-93229119c048', '2026-02-04T10:00:00', '2026-02-04T10:45:00', 'Tratamiento', 'Confirmado', 'EnCurso', 'Implante dental', '', 0, 'Seeds', NOW(), NULL, NULL),
(gen_random_uuid(), 'aca377cb-0007-0007-acf5-658609390007', '01429580-7e2d-43b5-ae88-93229119c048', '2026-02-05T09:00:00', '2026-02-05T09:30:00', 'Consulta', 'Programado', 'NoIniciado', 'Dolor al masticar', '', 0, 'Seeds', NOW(), NULL, NULL),
(gen_random_uuid(), 'aca377cb-0008-0008-acf5-658609390008', '01429580-7e2d-43b5-ae88-93229119c048', '2026-02-06T10:00:00', '2026-02-06T10:30:00', 'Consulta', 'Reprogramado', 'NoIniciado', 'Consulta cancelada por lluvia', '', 0, 'Seeds', NOW(), NULL, NULL),
(gen_random_uuid(), 'aca377cb-0009-0009-acf5-658609390009', '01429580-7e2d-43b5-ae88-93229119c048', '2026-02-07T08:30:00', '2026-02-07T09:00:00', 'Reconsulta', 'Confirmado', 'Completada', 'Revisión de caries tratada', '', 0, 'Seeds', NOW(), NULL, NULL),
(gen_random_uuid(), 'aca377cb-0010-0010-acf5-658609390010', '01429580-7e2d-43b5-ae88-93229119c048', '2026-02-08T11:00:00', '2026-02-08T11:45:00', 'Seguimiento', 'Programado', 'NoIniciado', 'Revisión general mensual', '', 0, 'Seeds', NOW(), NULL, NULL);
-- Total: 25 registros



-- ===============================
-- Crear Submodulos
-- ===============================
INSERT INTO public.submodule
("submodule_id", "submodule_name", "state", "created_at", "created_by")
VALUES
('1', 'Historia de Salud General', 'ACTIVE', NOW(), 'Seeds'),
('2', 'Historia de ortodoncia', 'ACTIVE', NOW(), 'Seeds'),
('3', 'Resumen de Tratamiento', 'ACTIVE', NOW(), 'Seeds'),
('4', 'Tratamiento de Ortodoncia', 'ACTIVE', NOW(), 'Seeds'),
('5', 'Tratamiento de Ortopedia', 'ACTIVE', NOW(), 'Seeds'),
('6', 'Tratamiento Miofuncional', 'ACTIVE', NOW(), 'Seeds'),
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

    -- Payment Treatment
    ('11111111-1111-1111-1111-111111111167', 'Paciente', 'Crear Pago', 'Permite crear pago de tratamiento', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111168', 'Paciente', 'Leer Pago', 'Permite leer pagos de tratamiento', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111169', 'Paciente', 'Actualizar Pago', 'Permite actualizar pago de tratamiento', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111170', 'Paciente', 'Eliminar Pago', 'Permite eliminar pago de tratamiento', 0, NOW(), 'Seeds');

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
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111166', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111167', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111168', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111169', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111170', 0, NOW(), 'Seeds');



-- ===============================
-- Asignar algunos permisos al rol Dentista
-- ===============================

INSERT INTO public.role_permission ("role_permission_id", "role_id", "permission_id", "state", "created_at", "created_by")
VALUES
    (gen_random_uuid(), 'a4b1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111111', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'a4b1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111112', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'a4b1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111113', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'a4b1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111114', 0, NOW(), 'Seeds');

