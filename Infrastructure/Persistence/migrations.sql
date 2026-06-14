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
(gen_random_uuid(), 'aca377cb-0007-0007-acf5-658609390007', '01429580-7e2d-43b5-ae88-93229119c048', '2026-01-09T11:00:00', '2026-01-09T11:45:00', 'Consulta', 'Confirmado', 'NoIniciado', 'Consulta general', '', 0, 'Seeds', NOW(), NULL, NULL),
(gen_random_uuid(), 'aca377cb-0008-0008-acf5-658609390008', '01429580-7e2d-43b5-ae88-93229119c048', '2026-01-10T08:30:00', '2026-01-10T09:00:00', 'Revision_de_progresos', 'Pendiente', 'NoIniciado', 'Evaluación mensual', '', 0, 'Seeds', NOW(), NULL, NULL),
(gen_random_uuid(), 'aca377cb-0009-0009-acf5-658609390009', '01429580-7e2d-43b5-ae88-93229119c048', '2026-01-11T10:15:00', '2026-01-11T10:45:00', 'Tratamiento', 'Programado', 'NoIniciado', 'Aplicación de resina', '', 0, 'Seeds', NOW(), NULL, NULL),
(gen_random_uuid(), 'aca377cb-0010-0010-acf5-658609390010', '01429580-7e2d-43b5-ae88-93229119c048', '2026-01-12T09:00:00', '2026-01-12T09:30:00', 'Emergencia', 'Confirmado', 'NoIniciado', 'Dolor post-tratamiento', '', 0, 'Seeds', NOW(), NULL, NULL),
(gen_random_uuid(), 'aca377cb-0001-0001-acf5-658609390001', '01429580-7e2d-43b5-ae88-93229119c048', '2026-01-13T11:00:00', '2026-01-13T11:30:00', 'Eliminacion_de_aparatos', 'Confirmado', 'Completada', 'Retiro de brackets', '', 0, 'Seeds', NOW(), NULL, NULL),
(gen_random_uuid(), 'aca377cb-0002-0002-acf5-658609390002', '01429580-7e2d-43b5-ae88-93229119c048', '2026-01-14T09:00:00', '2026-01-14T09:45:00', 'Tratamiento', 'Reprogramado', 'NoIniciado', 'Colocación de coronas', '', 0, 'Seeds', NOW(), NULL, NULL),
-- Febrero 2026
(gen_random_uuid(), 'aca377cb-0003-0003-acf5-658609390003', '01429580-7e2d-43b5-ae88-93229119c048', '2026-02-01T10:00:00', '2026-02-01T10:30:00', 'Consulta', 'Programado', 'NoIniciado', 'Consulta por revisión', '', 0, 'Seeds', NOW(), NULL, NULL),
(gen_random_uuid(), 'aca377cb-0004-0004-acf5-658609390004', '01429580-7e2d-43b5-ae88-93229119c048', '2026-02-02T09:00:00', '2026-02-02T09:30:00', 'Seguimiento', 'Confirmado', 'NoIniciado', 'Evaluar sensibilidad', '', 0, 'Seeds', NOW(), NULL, NULL),
(gen_random_uuid(), 'aca377cb-0005-0005-acf5-658609390005', '01429580-7e2d-43b5-ae88-93229119c048', '2026-02-03T11:00:00', '2026-02-03T11:30:00', 'Emergencia', 'Pendiente', 'NoIniciado', 'Inflamación dental', '', 0, 'Seeds', NOW(), NULL, NULL),
(gen_random_uuid(), 'aca377cb-0006-0006-acf5-658609390006', '01429580-7e2d-43b5-ae88-93229119c048', '2026-02-04T10:00:00', '2026-02-04T10:45:00', 'Tratamiento', 'Confirmado', 'NoIniciado', 'Implante dental', '', 0, 'Seeds', NOW(), NULL, NULL),
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
('6', 'Tratamiento Miofuncional', 'ACTIVE', NOW(), 'Seeds');
-- ('7', 'Presupuesto Dental', 'ACTIVE', NOW(), 'Seeds');

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
    ('11111111-1111-1111-1111-111111111166', 'Paciente', 'Eliminar Avance de tratamiento', 'Permite eliminar avance de tratamiento (pago)', 0, NOW(), 'Seeds'),

    -- Payment Treatment
    ('11111111-1111-1111-1111-111111111167', 'Paciente', 'Crear Pago', 'Permite crear pago de tratamiento', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111168', 'Paciente', 'Leer Pago', 'Permite leer pagos de tratamiento', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111169', 'Paciente', 'Actualizar Pago', 'Permite actualizar pago de tratamiento', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111170', 'Paciente', 'Eliminar Pago', 'Permite eliminar pago de tratamiento', 0, NOW(), 'Seeds'),

    -- Contract
    ('11111111-1111-1111-1111-111111111171', 'Paciente', 'Leer Contrato', 'Permite leer contrato', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111172', 'Paciente', 'Crear Contrato', 'Permite crear contrato', 0, NOW(), 'Seeds');



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
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111170', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111171', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111172', 0, NOW(), 'Seeds');



-- ===============================
-- Asignar algunos permisos al rol Dentista
-- ===============================

INSERT INTO public.role_permission ("role_permission_id", "role_id", "permission_id", "state", "created_at", "created_by")
VALUES
    (gen_random_uuid(), 'a4b1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111111', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'a4b1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111112', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'a4b1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111113', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'a4b1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111114', 0, NOW(), 'Seeds');



-- ===============================
-- Formularios
-- ===============================

INSERT INTO public.form ("form_id", "form_name", "form_description", "state", "created_at", "created_by")
VALUES
('00000000-0000-0000-0000-000000000001', 'Historia de Salud General', 'Formulario para la historia de salud general', 0, NOW(), 'Seeds'),
('00000000-0000-0000-0000-000000000002', 'Historia de ortodoncia', 'Formulario para la historia de ortodoncia', 0, NOW(), 'Seeds'),
('00000000-0000-0000-0000-000000000003', 'Resumen de Tratamiento', 'Formulario para el resumen de tratamiento', 0, NOW(), 'Seeds'),
('00000000-0000-0000-0000-000000000004', 'Tratamiento de Ortodoncia', 'Formulario para el tratamiento de ortodoncia', 0, NOW(), 'Seeds'),
('00000000-0000-0000-0000-000000000005', 'Tratamiento de Ortopedia', 'Formulario para el tratamiento de ortopedia', 0, NOW(), 'Seeds'),
('00000000-0000-0000-0000-000000000006', 'Tratamiento Miofuncional', 'Formulario para el tratamiento miofuncional', 0, NOW(), 'Seeds');


-- ===============================
-- Examen pretratratamiento
-- ===============================




-- ===============================
-- Versiones de formularios
-- ===============================

INSERT INTO public.form_version ("form_version_id", "form_id", "number_version","submodule_id", "json_schema", "state", "created_at", "created_by")
VALUES
(gen_random_uuid(), '00000000-0000-0000-0000-000000000001', '1', '1', '{
  "$schema": "http://json-schema.org/draft-07/schema#",
  "title": "Historia de Salud General",
  "type": "object",
  "properties": {
    "datosSaludGeneral": {
      "title": "DATOS DE SALUD GENERAL",
      "type": "object",
      "properties": {
        "alergicoMedicamentos": {
          "type": "boolean",
          "title": "¿Es alérgico (sensible) a medicamentos: penicilina, anestésicos, aspirina, etc.?",
          "default": false
        },
        "recibioAnestesia": {
          "type": "boolean",
          "title": "¿Ha recibido anestesia alguna vez?",
          "default": false
        },
        "embarazada": {
          "type": "boolean",
          "title": "¿Está embarazada?",
          "default": false
        },
        "tomaMedicamentos": {
          "type": "boolean",
          "title": "¿Toma regularmente algún medicamento?",
          "default": false
        },
        "vegetariano": {
          "type": "boolean",
          "title": "¿Es vegetariano(a)?",
          "default": false
        },
        "sangranEncias": {
          "type": "object",
          "title": "¿Le sangran las encías?",
          "properties": {
            "sangra": {
              "type": "boolean",
              "title": "¿Le sangran las encías?"
            },
            "tipo": {
              "type": "string",
              "title": "Tipo",
              "enum": ["espontáneamente", "al cepillado", "ambos"],
              "default": "al cepillado"
            }
          }
        },
        "malOlorBoca": {
          "type": "boolean",
          "title": "¿Siente que tiene mal olor en la boca?",
          "default": false
        },
        "antecedentes": {
          "type": "array",
          "title": "Indique si tiene antecedentes de (seleccionar):",
          "items": {
            "type": "string",
            "enum": [
              "rinitis alérgica",
              "enfermedad cardíaca",
              "hipertensión arterial",
              "fiebre reumática",
              "diabetes",
              "trastornos de la tiroides",
              "hepatitis",
              "neumonía",
              "asma"
            ]
          },
          "uniqueItems": true,
          "minItems": 0
        },
        "otrosAntecedentes": {
          "type": "string",
          "title": "Otros antecedentes:"
        },
        "detallesRelevantes": {
          "type": "string",
          "title": "Explicar detalles relevantes:",
          "ui:widget": "textarea",
          "ui:options": {
            "rows": 3
          }
        },
        "fuma": {
          "type": "boolean",
          "title": "¿Fuma?",
          "default": false
        },
        "cigarrosDia": {
          "type": "integer",
          "title": "¿Cuántos cigarrillos al día?",
          "minimum": 0,
          "maximum": 100
        },
        "dolorArticulacionCara": {
          "type": "boolean",
          "title": "¿Tiene dolor en la articulación o músculos de la cara?",
          "default": false
        },
        "fiebre14dias": {
          "type": "boolean",
          "title": "¿Tiene o ha tenido fiebre en los últimos 14 días?",
          "default": false
        },
        "problemasRespiratorios": {
          "type": "boolean",
          "title": "¿Ha tenido problemas respiratorios incluyendo tos en los últimos 14 días?",
          "default": false
        },
        "contactoPersonaEnferma": {
          "type": "boolean",
          "title": "¿Ha estado en contacto con alguna persona con fiebre, tos o dificultad para respirar?",
          "default": false
        },
        "contactoCovid19": {
          "type": "boolean",
          "title": "¿Ha estado en contacto con alguna persona con COVID-19?",
          "default": false
        },
        "nombreOdontologo": {
          "type": "string",
          "title": "Nombre de su odontólogo"
        },
        "telefonoOdontologo": {
          "type": "string",
          "title": "Teléfono del odontólogo"
        },
        "nombreMedico": {
          "type": "string",
          "title": "Nombre de su médico personal"
        },
        "telefonoMedico": {
          "type": "string",
          "title": "Teléfono del médico"
        }
      },
      "required": [
        "alergicoMedicamentos",
        "recibioAnestesia",
        "embarazada",
        "tomaMedicamentos",
        "vegetariano",
        "fuma",
        "fiebre14dias",
        "problemasRespiratorios",
        "contactoPersonaEnferma",
        "contactoCovid19"
      ]
    }
  },
  "required": [
    "datosSaludGeneral"
  ]
}', 0, NOW(), 'Seeds'),

(gen_random_uuid(), '00000000-0000-0000-0000-000000000002', '2', '2', '{
  "$schema": "http://json-schema.org/draft-07/schema#",
  "title": "Historia Clínica Dental Completa",
  "type": "object",
  "properties": {
    "datosGenerales": {
      "title": "HISTORIA DE SALUD GENERAL",
      "type": "object",
      "properties": {
        "datosSaludGeneral": {
          "title": "DATOS DE SALUD GENERAL",
          "type": "object",
          "properties": {
            "alergicoMedicamentos": {
              "type": "boolean",
              "title": "¿Es alérgico (sensible) a medicamentos: penicilina, anestésicos, aspirina, etc.?"
            },
            "recibioAnestesia": {
              "type": "boolean",
              "title": "¿Ha recibido anestesia alguna vez?"
            },
            "embarazada": {
              "type": "boolean",
              "title": "¿Está embarazada?"
            },
            "tomaMedicamentos": {
              "type": "boolean",
              "title": "¿Toma regularmente algún medicamento?"
            },
            "vegetariano": {
              "type": "boolean",
              "title": "¿Es vegetariano(a)?"
            },
            "sangranEncias": {
              "type": "boolean",
              "title": "¿Le sangran las encías?"
            },
            "malOlorBoca": {
              "type": "boolean",
              "title": "¿Siente que tiene mal olor en la boca?"
            },
            "antecedentes": {
              "type": "array",
              "title": "Antecedentes de",
              "items": {
                "type": "string",
                "enum": [
                  "rinitis alérgica",
                  "enfermedad cardíaca",
                  "hipertensión arterial",
                  "fiebre reumática",
                  "diabetes",
                  "trastornos de la tiroides",
                  "hepatitis",
                  "neumonía",
                  "asma",
                  "otros"
                ]
              },
              "uniqueItems": true
            },
            "otrosAntecedentes": {
              "type": "string",
              "title": "Otros antecedentes"
            },
            "detallesRelevantes": {
              "type": "string",
              "title": "Explicar detalles relevantes"
            },
            "fuma": {
              "type": "boolean",
              "title": "¿Fuma?"
            },
            "cigarrosDia": {
              "type": "integer",
              "title": "¿Cuántos cigarrillos al día?"
            },
            "dolorArticulacionCara": {
              "type": "boolean",
              "title": "¿Tiene dolor en la articulación o músculos de la cara?"
            },
            "fiebre14dias": {
              "type": "boolean",
              "title": "¿Tiene o ha tenido fiebre en los últimos 14 días?"
            },
            "problemasRespiratorios": {
              "type": "boolean",
              "title": "¿Ha tenido problemas respiratorios incluyendo tos en los últimos 14 días?"
            },
            "contactoPersonaEnferma": {
              "type": "boolean",
              "title": "¿Ha estado en contacto con alguna persona con fiebre, tos o dificultad para respirar?"
            },
            "contactoCovid19": {
              "type": "boolean",
              "title": "¿Ha estado en contacto con alguna persona con COVID-19?"
            },
            "nombreOdontologo": {
              "type": "string",
              "title": "Nombre de su odontólogo"
            },
            "telefonoOdontologo": {
              "type": "string",
              "title": "Teléfono del odontólogo"
            },
            "nombreMedico": {
              "type": "string",
              "title": "Nombre de su médico personal"
            },
            "telefonoMedico": {
              "type": "string",
              "title": "Teléfono del médico"
            }
          }
        }
      }
    },
    "examenPretratamiento": {
      "title": "EXAMEN DENTAL PRE-TRATAMIENTO",
      "type": "object",
      "properties": {
        "nombrePaciente": {
          "type": "string",
          "title": "Nombre del Paciente"
        },
        "registrosDentales": {
          "type": "array",
          "title": "Registro de piezas dentales",
          "items": {
            "type": "object",
            "properties": {
              "fecha": {
                "type": "string",
                "format": "date",
                "title": "Fecha"
              },
              "pieza": {
                "type": "string",
                "title": "Pieza"
              },
              "caries": {
                "type": "string",
                "title": "Caries"
              },
              "tratamiento": {
                "type": "string",
                "title": "Tratamiento"
              },
              "costo": {
                "type": "number",
                "title": "Costo"
              }
            }
          }
        },
        "interconsulta": {
          "type": "string",
          "title": "INTERCONSULTA"
        },
        "observaciones": {
          "type": "string",
          "title": "OBSERVACIONES"
        }
      }
    },
    "historiaOrtodoncia": {
      "title": "HISTORIA CLÍNICA DE ORTODONCIA",
      "type": "object",
      "properties": {
        "datosBasicos": {
          "title": "DATOS BÁSICOS",
          "type": "object",
          "properties": {
            "nombresApellidosOrt": {
              "type": "string",
              "title": "Nombres y apellidos"
            },
            "estado": {
              "type": "string",
              "title": "Estado"
            },
            "fechaNacimientoOrt": {
              "type": "string",
              "format": "date",
              "title": "Fecha de nacimiento"
            },
            "edadOrt": {
              "type": "integer",
              "title": "Edad"
            },
            "telefonoOrt": {
              "type": "string",
              "title": "Teléfono"
            },
            "motivoConsultaOrt": {
              "type": "string",
              "title": "Motivo de la consulta"
            },
            "profesionalRemitioOrt": {
              "type": "string",
              "title": "Profesional o persona que le remitió"
            }
          }
        },
        "preguntasPaciente": {
          "title": "PREGUNTAS PARA EL PACIENTE",
          "type": "object",
          "properties": {
            "alergiaMedicamentos": { "type": "boolean", "title": "¿Tiene alergia a algún medicamento, anestesia o metales?" },
            "parecidoFamiliar": { "type": "boolean", "title": "¿Tiene parecido facial o dental con alguien de la familia?" },
            "tratamientoOrtodonciaPrev": { "type": "boolean", "title": "¿Ha recibido anteriormente algún tratamiento de ortodoncia?" },
            "familiaTratamientoOrt": { "type": "boolean", "title": "¿Alguno de la familia ha recibido tratamiento de ortodoncia?" },
            "rechinaDientes": { "type": "boolean", "title": "¿Rechina o aprieta los dientes durante el día o la noche?" },
            "tratamientoMedico": { "type": "boolean", "title": "¿Está bajo tratamiento médico?" },
            "tomaMedicamentosEspecificos": { "type": "boolean", "title": "¿Toma anti-histamínicos, anti-inflamatorios, hormonas, calcio, etc.?" },
            "hospitalizado": { "type": "boolean", "title": "¿Ha estado hospitalizado(a)?" },
            "diabetes": { "type": "boolean", "title": "¿Tiene Ud. diabetes?" },
            "sangranEnciasOrt": { "type": "boolean", "title": "¿Le sangran las encías con facilidad?" },
            "vegetarianoOrt": { "type": "boolean", "title": "¿Es vegetariano(a)?" },
            "fumaOrt": { "type": "boolean", "title": "¿Ud. fuma?" },
            "traumatismosCara": { "type": "boolean", "title": "¿Tuvo accidentes o traumatismos en la cara?" },
            "cambioVoz": { "type": "boolean", "title": "¿Cambió la voz (adolescentes)?" },
            "comenzoMenstruacion": { "type": "boolean", "title": "¿Ha comenzado la menstruación? (mujeres)" },
            "embarazadaOrt": { "type": "boolean", "title": "¿Está usted embarazada? (mujeres)" }
          }
        },
        "medidasCorporales": {
          "title": "MEDIDAS CORPORALES",
          "type": "object",
          "properties": {
            "estaturaPaciente": {
              "type": "number",
              "title": "Estatura del paciente (cm)"
            },
            "peso": {
              "type": "number",
              "title": "Peso (Kg.)"
            },
            "expectativaCrecimiento": {
              "type": "string",
              "title": "Expectativa de crecimiento"
            },
            "estaturaPadre": {
              "type": "number",
              "title": "Estatura del padre (cm)"
            },
            "estaturaMadre": {
              "type": "number",
              "title": "Estatura de la madre (cm)"
            },
            "ultimoControlDental": {
              "type": "string",
              "format": "date",
              "title": "Fecha del último control dental"
            }
          }
        }
      }
    },
    "analisisFacial": {
      "title": "ANÁLISIS DE PERFIL Y CARA",
      "type": "object",
      "properties": {
        "proporcionesCefalicas": {
          "type": "string",
          "title": "Proporciones cefálicas",
          "enum": ["Mesocéfalo", "Braquicéfalo", "Dolicocéfalo"]
        },
        "simetriaFacial": {
          "type": "string",
          "title": "Simetría facial",
          "enum": ["Simétrico", "Asimétrico"]
        },
        "convexidadFacial": {
          "type": "string",
          "title": "Convexidad facial",
          "enum": ["Ortognático", "Convexo", "Cóncavo"]
        },
        "divergenciaFacial": {
          "type": "string",
          "title": "Divergencia facial",
          "enum": ["Recto", "Anterior", "Posterior"]
        },
        "posturaLabial": {
          "type": "string",
          "title": "Postura labial",
          "enum": ["Competente", "Superior corto", "Inferior protrusivo", "Incompetente", "BI protrusión"]
        },
        "tamanoNariz": {
          "type": "string",
          "title": "Tamaño relativo de la nariz",
          "enum": ["Promedio", "Pequeño", "Grande", "Se indica plastia"]
        },
        "aperturaBucalMax": {
          "type": "integer",
          "title": "Apertura bucal máxima (mm)"
        },
        "overbite": {
          "type": "integer",
          "title": "Overbite (mm)"
        },
        "desviacionMandibular": {
          "type": "object",
          "title": "Desviación mandibular",
          "properties": {
            "apertura": {
              "type": "array",
              "title": "En apertura",
              "items": {
                "type": "string",
                "enum": ["Derecha", "Izquierda"]
              },
              "uniqueItems": true
            },
            "cierre": {
              "type": "array",
              "title": "En cierre",
              "items": {
                "type": "string",
                "enum": ["Derecha", "Izquierda"]
              },
              "uniqueItems": true
            }
          }
        }
      }
    }
  }
}', 0, NOW(), 'Seeds'),

(gen_random_uuid(), '00000000-0000-0000-0000-000000000003', '3', '3', '{
  "$schema": "http://json-schema.org/draft-07/schema#",
  "title": "Resumen Integral de Tratamiento Odontológico",
  "description": "Documento de planificación y seguimiento del tratamiento dental",
  "type": "object",
  "properties": {
    "evaluacionInicial": {
      "title": "EVALUACIÓN INICIAL",
      "type": "object",
      "properties": {
        "motivoConsulta": {
          "type": "string",
          "title": "Motivo principal de consulta",
          "ui:widget": "textarea",
          "ui:options": {
            "rows": 2
          }
        },
        "expectativas": {
          "type": "string",
          "title": "Expectativas del paciente",
          "ui:widget": "textarea",
          "ui:options": {
            "rows": 2
          }
        },
        "prioridadTratamiento": {
          "type": "string",
          "title": "Prioridad de tratamiento",
          "enum": ["Urgente", "Alta", "Media", "Baja", "Electivo"],
          "default": "Media"
        },
        "complejidad": {
          "type": "string",
          "title": "Nivel de complejidad",
          "enum": ["Baja", "Moderada", "Alta", "Muy Alta"],
          "default": "Moderada"
        },
        "riesgosIdentificados": {
          "type": "array",
          "title": "Factores de riesgo identificados",
          "items": {
            "type": "string",
            "enum": [
              "Enfermedad periodontal activa",
              "Alta carga cariogénica",
              "Malos hábitos de higiene",
              "Patología sistémica",
              "Tabaquismo",
              "Bruxismo",
              "Ansiedad dental",
              "Alergias medicamentosas",
              "Limitaciones económicas",
              "Baja colaboración"
            ]
          },
          "uniqueItems": true,
          "minItems": 0
        }
      },
      "required": ["motivoConsulta", "prioridadTratamiento"]
    },
    "diagnosticos": {
      "title": "DIAGNÓSTICOS ESTABLECIDOS",
      "type": "object",
      "properties": {
        "diagnosticoPrincipal": {
          "type": "string",
          "title": "Diagnóstico principal (CIE-10)"
        },
        "diagnosticosSecundarios": {
          "type": "array",
          "title": "Diagnósticos secundarios",
          "items": {
            "type": "string"
          }
        },
        "clasificacionOclusal": {
          "type": "object",
          "title": "Clasificación oclusal",
          "properties": {
            "claseAngle": {
              "type": "string",
              "title": "Clase de Angle",
              "enum": ["Clase I", "Clase II Div 1", "Clase II Div 2", "Clase III"]
            },
            "sobremordida": {
              "type": "string",
              "title": "Sobremordida",
              "enum": ["Normal", "Aumentada", "Disminuida", "Mordida abierta"]
            },
            "resalte": {
              "type": "string",
              "title": "Resalte",
              "enum": ["Normal", "Aumentado", "Invertido"]
            }
          }
        },
        "hallazgosRadiograficos": {
          "type": "string",
          "title": "Hallazgos radiográficos relevantes",
          "ui:widget": "textarea",
          "ui:options": {
            "rows": 3
          }
        }
      },
      "required": ["diagnosticoPrincipal"]
    },
    "planTratamiento": {
      "title": "PLAN DE TRATAMIENTO",
      "type": "object",
      "properties": {
        "fases": {
          "type": "array",
          "title": "Fases del tratamiento",
          "items": {
            "type": "object",
            "properties": {
              "orden": {
                "type": "integer",
                "title": "Orden",
                "minimum": 1
              },
              "fase": {
                "type": "string",
                "title": "Fase",
                "enum": [
                  "Urgencia",
                  "Fase Higiénica",
                  "Control de Enfermedad",
                  "Preparación",
                  "Rehabilitación",
                  "Estabilización",
                  "Mantenimiento"
                ]
              },
              "objetivo": {
                "type": "string",
                "title": "Objetivo específico"
              },
              "procedimientos": {
                "type": "array",
                "title": "Procedimientos planificados",
                "items": {
                  "type": "string"
                }
              },
              "tiempoEstimado": {
                "type": "string",
                "title": "Tiempo estimado",
                "description": "Ej: 2-4 semanas, 3 meses"
              }
            },
            "required": ["orden", "fase", "objetivo"]
          }
        },
        "objetivosFuncionales": {
          "type": "string",
          "title": "Objetivos funcionales",
          "ui:widget": "textarea",
          "ui:options": {
            "rows": 2
          }
        },
        "objetivosEsteticos": {
          "type": "string",
          "title": "Objetivos estéticos",
          "ui:widget": "textarea",
          "ui:options": {
            "rows": 2
          }
        },
        "criteriosExito": {
          "type": "array",
          "title": "Criterios de éxito",
          "items": {
            "type": "string"
          }
        }
      },
      "required": ["fases"]
    },
    "aspectosTecnicos": {
      "title": "ASPECTOS TÉCNICOS",
      "type": "object",
      "properties": {
        "tecnicaPrincipal": {
          "type": "string",
          "title": "Técnica principal a utilizar"
        },
        "materialesEspeciales": {
          "type": "array",
          "title": "Materiales especiales requeridos",
          "items": {
            "type": "string"
          }
        },
        "requiereLaboratorio": {
          "type": "boolean",
          "title": "¿Requiere trabajo de laboratorio?"
        },
        "laboratorioAsignado": {
          "type": "string",
          "title": "Laboratorio dental asignado"
        },
        "necesitaInterconsulta": {
          "type": "boolean",
          "title": "¿Requiere interconsulta con otro especialista?"
        },
        "especialistasInvolucrados": {
          "type": "array",
          "title": "Especialistas involucrados",
          "items": {
            "type": "string",
            "enum": [
              "Periodoncista",
              "Endodoncista",
              "Cirujano Maxilofacial",
              "Prostodoncista",
              "Ortodoncista",
              "Anestesiólogo",
              "Médico Internista"
            ]
          },
          "uniqueItems": true
        }
      }
    },
    "seguimientoEvaluacion": {
      "title": "SEGUIMIENTO Y EVALUACIÓN",
      "type": "object",
      "properties": {
        "frecuenciaControles": {
          "type": "string",
          "title": "Frecuencia de controles",
          "enum": [
            "Semanal",
            "Quincenal",
            "Mensual",
            "Bimestral",
            "Trimestral",
            "Según necesidad"
          ],
          "default": "Mensual"
        },
        "indicadoresSeguimiento": {
          "type": "array",
          "title": "Indicadores de seguimiento",
          "items": {
            "type": "string",
            "enum": [
              "Índice de placa",
              "Índice gingival",
              "Movilidad dental",
              "Profundidad de sondaje",
              "Nivel de satisfacción",
              "Adherencia al tratamiento"
            ]
          },
          "uniqueItems": true
        },
        "citasEvaluacion": {
          "type": "array",
          "title": "Citas de evaluación programadas",
          "items": {
            "type": "object",
            "properties": {
              "fecha": {
                "type": "string",
                "format": "date",
                "title": "Fecha"
              },
              "tipo": {
                "type": "string",
                "title": "Tipo de evaluación",
                "enum": [
                  "Evaluación intermedia",
                  "Control de higiene",
                  "Evaluación radiográfica",
                  "Evaluación final",
                  "Mantenimiento"
                ]
              },
              "objetivo": {
                "type": "string",
                "title": "Objetivo específico"
              }
            },
            "required": ["fecha", "tipo"]
          }
        }
      }
    },
    "pronostico": {
      "title": "PRONÓSTICO Y CONSIDERACIONES",
      "type": "object",
      "properties": {
        "pronosticoGeneral": {
          "type": "string",
          "title": "Pronóstico general",
          "enum": ["Excelente", "Bueno", "Regular", "Reservado", "Desfavorable"],
          "default": "Bueno"
        },
        "factoresFavorables": {
          "type": "array",
          "title": "Factores favorables",
          "items": {
            "type": "string"
          }
        },
        "factoresLimitantes": {
          "type": "array",
          "title": "Factores limitantes",
          "items": {
            "type": "string"
          }
        },
        "probabilidadExito": {
          "type": "integer",
          "title": "Probabilidad de éxito estimada (%)",
          "minimum": 0,
          "maximum": 100,
          "default": 80
        },
        "observacionesEspeciales": {
          "type": "string",
          "title": "Observaciones especiales",
          "ui:widget": "textarea",
          "ui:options": {
            "rows": 3
          }
        }
      },
      "required": ["pronosticoGeneral", "probabilidadExito"]
    },
    "consentimiento": {
      "title": "CONSENTIMIENTO Y AUTORIZACIONES",
      "type": "object",
      "properties": {
        "informado": {
          "type": "boolean",
          "title": "Consentimiento informado obtenido"
        },
        "fechaConsentimiento": {
          "type": "string",
          "format": "date",
          "title": "Fecha de consentimiento"
        },
        "aceptaPlan": {
          "type": "boolean",
          "title": "Acepta el plan de tratamiento propuesto"
        },
        "autorizaFotografias": {
          "type": "boolean",
          "title": "Autoriza uso de fotografías para registro clínico"
        },
        "observacionesPaciente": {
          "type": "string",
          "title": "Observaciones del paciente",
          "ui:widget": "textarea",
          "ui:options": {
            "rows": 2
          }
        }
      },
      "required": ["informado", "aceptaPlan"]
    }
  },
  "required": [
    "evaluacionInicial",
    "diagnosticos",
    "planTratamiento",
    "pronostico",
    "consentimiento"
  ]
}', 0, NOW(), 'Seeds'),

(gen_random_uuid(), '00000000-0000-0000-0000-000000000004', '4', '4', '{
  "$schema": "http://json-schema.org/draft-07/schema#",
  "title": "Contrato Integral de Tratamiento de Ortodoncia",
  "type": "object",
  "properties": {
    "detallesTratamiento": {
      "title": "DETALLES DEL TRATAMIENTO",
      "type": "object",
      "properties": {
        "modalidadAparatologia": {
          "type": "string",
          "title": "Modalidad de aparatología",
          "enum": [
            "Brackets metálicos convencionales",
            "Brackets estéticos (cerámica/zafiro)",
            "Brackets autoligables",
            "Sistema lingual",
            "Alineadores transparentes (Invisalign)",
            "Aparatología removible"
          ]
        },
        "arcadaTratada": {
          "type": "string",
          "title": "Arcada(s) a tratar",
          "enum": ["Superior", "Inferior", "Ambas arcadas"],
          "default": "Ambas arcadas"
        },
        "extraccionesRequeridas": {
          "type": "boolean",
          "title": "¿Requiere extracciones dentales?"
        },
        "detallesExtracciones": {
          "type": "string",
          "title": "Detalles de extracciones (si aplica)"
        },
        "tiempoEstimadoTotal": {
          "type": "object",
          "title": "Tiempo estimado total",
          "properties": {
            "meses": {
              "type": "integer",
              "title": "Meses",
              "minimum": 1,
              "maximum": 48
            },
            "rango": {
              "type": "string",
              "title": "Rango estimado",
              "description": "Ej: 18-24 meses"
            }
          }
        },
        "citasEstimadas": {
          "type": "integer",
          "title": "Número estimado de citas",
          "minimum": 1,
          "maximum": 100
        }
      },
      "required": ["modalidadAparatologia", "arcadaTratada", "tiempoEstimadoTotal"]
    },
    "politicasContrato": {
      "title": "POLÍTICAS DEL CONTRATO",
      "type": "object",
      "properties": {
        "periodoGarantia": {
          "type": "object",
          "title": "Período de garantía",
          "properties": {
            "meses": {
              "type": "integer",
              "title": "Meses de garantía",
              "minimum": 0,
              "maximum": 36
            },
            "cubre": {
              "type": "array",
              "title": "¿Qué cubre la garantía?",
              "items": {
                "type": "string",
                "enum": [
                  "Rotura de brackets",
                  "Descementado de brackets",
                  "Rotura de arcos",
                  "Ajustes menores",
                  "Controles post-tratamiento"
                ]
              }
            }
          }
        },
        "politicaCancelacion": {
          "type": "object",
          "title": "Política de cancelación",
          "properties": {
            "diasRetracto": {
              "type": "integer",
              "title": "Días de derecho de retracto",
              "minimum": 0,
              "maximum": 30
            },
            "porcentajePenalidad": {
              "type": "number",
              "title": "Penalidad por cancelación (%)",
              "minimum": 0,
              "maximum": 100
            },
            "condicionesReembolso": {
              "type": "string",
              "title": "Condiciones de reembolso",
              "ui:widget": "textarea"
            }
          }
        },
        "politicaInasistencias": {
          "type": "object",
          "title": "Política de inasistencias",
          "properties": {
            "toleranciaMinutos": {
              "type": "integer",
              "title": "Tolerancia (minutos)",
              "minimum": 0,
              "maximum": 60
            },
            "cargoCancelacionTardia": {
              "type": "number",
              "title": "Cargo por cancelación tardía"
            },
            "maximoInasistencias": {
              "type": "integer",
              "title": "Máximo de inasistencias permitidas"
            }
          }
        }
      }
    },
    "responsabilidades": {
      "title": "RESPONSABILIDADES",
      "type": "object",
      "properties": {
        "responsabilidadesPaciente": {
          "type": "array",
          "title": "Responsabilidades del paciente",
          "items": {
            "type": "string"
          },
          "default": [
            "Asistir a todas las citas programadas",
            "Mantener excelente higiene oral",
            "Seguir instrucciones dietéticas",
            "Usar elásticos según indicación",
            "Reportar cualquier problema inmediatamente"
          ]
        },
        "responsabilidadesClinica": {
          "type": "array",
          "title": "Responsabilidades de la clínica",
          "items": {
            "type": "string"
          },
          "default": [
            "Proporcionar tratamiento de calidad",
            "Mantener equipos en óptimas condiciones",
            "Respetar horarios acordados",
            "Proporcionar educación al paciente",
            "Mantener confidencialidad de datos"
          ]
        }
      }
    },
    "seguimientoContencion": {
      "title": "SEGUIMIENTO Y CONTENCIÓN",
      "type": "object",
      "properties": {
        "planContencion": {
          "type": "object",
          "title": "Plan de contención post-tratamiento",
          "properties": {
            "tipoContenedor": {
              "type": "string",
              "title": "Tipo de contenedor",
              "enum": [
                "Retenedor fijo",
                "Retenedor removible Hawley",
                "Retenedor Essix",
                "Placa de contención",
                "Combinación"
              ]
            },
            "duracionUso": {
              "type": "string",
              "title": "Duración de uso",
              "description": "Ej: 24 horas/día por 6 meses, luego nocturno"
            },
            "costoIncluido": {
              "type": "boolean",
              "title": "¿Incluido en el costo total?"
            }
          }
        },
        "seguimientoPosterior": {
          "type": "object",
          "title": "Seguimiento posterior",
          "properties": {
            "controlesIncluidos": {
              "type": "integer",
              "title": "Controles incluidos (meses)",
              "minimum": 0,
              "maximum": 24
            },
            "frecuenciaControles": {
              "type": "string",
              "title": "Frecuencia de controles"
            }
          }
        }
      }
    },
    "firmasAutorizaciones": {
      "title": "FIRMAS Y AUTORIZACIONES",
      "type": "object",
      "properties": {
        "aceptacionClausulas": {
          "type": "object",
          "title": "Aceptación de cláusulas",
          "properties": {
            "aceptaTerminos": {
              "type": "boolean",
              "title": "Acepta los términos del contrato"
            },
            "autorizaTratamiento": {
              "type": "boolean",
              "title": "Autoriza el tratamiento"
            },
            "consienteFotos": {
              "type": "boolean",
              "title": "Consiente uso de fotografías clínicas"
            },
            "aceptaContacto": {
              "type": "boolean",
              "title": "Autoriza contacto para seguimiento"
            }
          }
        },
        "informacionFirmantes": {
          "type": "object",
          "title": "Información de los firmantes",
          "properties": {
            "nombrePaciente": {
              "type": "string",
              "title": "Nombre completo del paciente"
            },
            "nombreResponsable": {
              "type": "string",
              "title": "Nombre del responsable (si aplica)"
            },
            "parentesco": {
              "type": "string",
              "title": "Parentesco"
            },
            "nombreOdontologo": {
              "type": "string",
              "title": "Nombre del odontólogo tratante"
            }
          }
        }
      },
      "required": ["aceptacionClausulas", "informacionFirmantes"]
    }
  },
  "required": [
    "detallesTratamiento",
    "firmasAutorizaciones"
  ]
}', 0, NOW(), 'Seeds'),

(gen_random_uuid(), '00000000-0000-0000-0000-000000000005', '5', '5', '{
  "$schema": "http://json-schema.org/draft-07/schema#",
  "title": "Contrato de Tratamiento de Ortopedia Maxilar",
  "description": "Contrato para tratamientos de ortopedia en pacientes en crecimiento",
  "type": "object",
  "properties": {
    "moduloInformacion": {
      "title": "MÓDULO DE INFORMACIÓN",
      "type": "object",
      "properties": {
        "fechaInicio": {
          "type": "string",
          "format": "date",
          "title": "Fecha de inicio estimada"
        },
        "edadPaciente": {
          "type": "integer",
          "title": "Edad del paciente",
          "minimum": 4,
          "maximum": 18
        },
        "etapaDental": {
          "type": "string",
          "title": "Etapa dental",
          "enum": [
            "Dentición mixta temprana",
            "Dentición mixta tardía",
            "Dentición permanente incipiente",
            "Dentición permanente"
          ]
        },
        "etapaCrecimiento": {
          "type": "string",
          "title": "Etapa de crecimiento",
          "enum": [
            "Pre-pico",
            "Pico de crecimiento",
            "Post-pico",
            "Crecimiento completado"
          ]
        }
      },
      "required": ["codigoContrato", "fechaInicio", "edadPaciente"]
    },
    "moduloTratamiento": {
      "title": "MÓDULO DE TRATAMIENTO",
      "type": "object",
      "properties": {
        "objetivosPrimarios": {
          "type": "array",
          "title": "Objetivos primarios de tratamiento",
          "items": {
            "type": "string",
            "enum": [
              "Corrección de mordida cruzada",
              "Expansión maxilar",
              "Estimulación del crecimiento mandibular",
              "Restricción del crecimiento maxilar",
              "Corrección de hábitos",
              "Mejora de la respiración",
              "Espacio para erupción",
              "Corrección asimetrías"
            ]
          },
          "minItems": 1,
          "uniqueItems": true
        },
        "aparatosSeleccionados": {
          "type": "array",
          "title": "Aparatos seleccionados",
          "items": {
            "type": "object",
            "properties": {
              "aparato": {
                "type": "string",
                "title": "Nombre del aparato",
                "enum": [
                  "Expansor Hyrax",
                  "Quad Helix",
                  "Bionator",
                  "Activador",
                  "Twin Block",
                  "Máscara facial",
                  "Chin Cup",
                  "Placa Hawley modificada"
                ]
              },
              "duracionUso": {
                "type": "string",
                "title": "Duración estimada de uso"
              },
              "horasDiarias": {
                "type": "integer",
                "title": "Horas de uso diario",
                "minimum": 0,
                "maximum": 24
              }
            }
          }
        },
        "fasesActivas": {
          "type": "array",
          "title": "Fases activas de tratamiento",
          "items": {
            "type": "object",
            "properties": {
              "fase": {
                "type": "string",
                "title": "Fase"
              },
              "duracion": {
                "type": "string",
                "title": "Duración"
              },
              "objetivo": {
                "type": "string",
                "title": "Objetivo específico"
              }
            }
          }
        }
      },
      "required": ["objetivosPrimarios", "aparatosSeleccionados"]
    },
    "moduloCompromisos": {
      "title": "MÓDULO DE COMPROMISOS",
      "type": "object",
      "properties": {
        "compromisoPaciente": {
          "type": "object",
          "title": "Compromiso del paciente/familia",
          "properties": {
            "horasUsoDiario": {
              "type": "integer",
              "title": "Compromiso de horas de uso diario",
              "minimum": 0,
              "maximum": 24
            },
            "asistenciaCitas": {
              "type": "boolean",
              "title": "Compromiso de asistir a todas las citas"
            },
            "mantenimientoAparato": {
              "type": "boolean",
              "title": "Compromiso de mantenimiento del aparato"
            },
            "seguimientoInstrucciones": {
              "type": "boolean",
              "title": "Compromiso de seguir instrucciones"
            }
          }
        },
        "compromisoClinica": {
          "type": "object",
          "title": "Compromiso de la clínica",
          "properties": {
            "tiempoRespuesta": {
              "type": "integer",
              "title": "Tiempo máximo de respuesta a urgencias (horas)",
              "minimum": 1,
              "maximum": 72
            },
            "disponibilidad": {
              "type": "string",
              "title": "Horarios de disponibilidad"
            },
            "entregaInformes": {
              "type": "string",
              "title": "Compromiso de entrega de informes"
            }
          }
        }
      }
    },
    "moduloEvaluacion": {
      "title": "MÓDULO DE EVALUACIÓN",
      "type": "object",
      "properties": {
        "criteriosEvaluacion": {
          "type": "array",
          "title": "Criterios de evaluación del progreso",
          "items": {
            "type": "string",
            "enum": [
              "Crecimiento óseo observado",
              "Mejoría en la oclusión",
              "Corrección de hábitos",
              "Cooperación del paciente",
              "Estado de los tejidos blandos",
              "Progreso radiográfico"
            ]
          }
        },
        "evaluacionesProgramadas": {
          "type": "array",
          "title": "Evaluaciones programadas",
          "items": {
            "type": "object",
            "properties": {
              "mes": {
                "type": "integer",
                "title": "Mes del tratamiento"
              },
              "tipoEvaluacion": {
                "type": "string",
                "title": "Tipo de evaluación"
              },
              "proposito": {
                "type": "string",
                "title": "Propósito"
              }
            }
          }
        }
      }
    }
  },
  "required": [
    "moduloInformacion",
    "moduloTratamiento",
    "moduloCompromisos"
  ]
}', 0, NOW(), 'Seeds'),

(gen_random_uuid(), '00000000-0000-0000-0000-000000000006', '6', '6', '{
  "$schema": "http://json-schema.org/draft-07/schema#",
  "title": "Contrato de Terapia Miofuncional Orofacial",
  "description": "Acuerdo para tratamiento de desórdenes miofuncionales orofaciales",
  "type": "object",
  "properties": {
    "evaluacionInicial": {
      "title": "EVALUACIÓN INICIAL",
      "type": "object",
      "properties": {
        "fechaEvaluacion": {
          "type": "string",
          "format": "date",
          "title": "Fecha de evaluación"
        },
        "terapeutaResponsable": {
          "type": "string",
          "title": "Terapeuta miofuncional"
        },
        "diagnosticoMiofuncional": {
          "type": "array",
          "title": "Diagnóstico(s) miofuncional(es)",
          "items": {
            "type": "string",
            "enum": [
              "Deglución atípica",
              "Respiración bucal",
              "Interposición lingual",
              "Succión digital",
              "Bruxismo",
              "Incompetencia labial",
              "Hipertonía muscular",
              "Hipotonía muscular",
              "Alteración postural"
            ]
          },
          "minItems": 1,
          "uniqueItems": true
        },
        "edadTerapia": {
          "type": "string",
          "title": "Edad ideal para terapia",
          "enum": [
            "Pre-escolar (3-5 años)",
            "Escolar (6-12 años)",
            "Adolescente (13-18 años)",
            "Adulto (>18 años)"
          ]
        }
      },
      "required": ["fechaEvaluacion", "terapeutaResponsable", "diagnosticoMiofuncional"]
    },
    "planTerapeutico": {
      "title": "PLAN TERAPÉUTICO",
      "type": "object",
      "properties": {
        "objetivosTerapia": {
          "type": "object",
          "title": "Objetivos de la terapia",
          "properties": {
            "cortoPlazo": {
              "type": "array",
              "title": "Objetivos a corto plazo (1-3 meses)",
              "items": {
                "type": "string"
              }
            },
            "medianoPlazo": {
              "type": "array",
              "title": "Objetivos a mediano plazo (4-6 meses)",
              "items": {
                "type": "string"
              }
            },
            "largoPlazo": {
              "type": "array",
              "title": "Objetivos a largo plazo (>6 meses)",
              "items": {
                "type": "string"
              }
            }
          }
        },
        "frecuenciaSesiones": {
          "type": "object",
          "title": "Frecuencia de sesiones",
          "properties": {
            "sesionesSemanales": {
              "type": "integer",
              "title": "Sesiones por semana",
              "minimum": 1,
              "maximum": 5
            },
            "duracionSesion": {
              "type": "integer",
              "title": "Duración de cada sesión (minutos)",
              "minimum": 30,
              "maximum": 120
            },
            "modalidad": {
              "type": "string",
              "title": "Modalidad",
              "enum": ["Presencial", "Virtual", "Mixta"]
            }
          }
        },
        "ejerciciosPrescritos": {
          "type": "array",
          "title": "Ejercicios prescritos inicialmente",
          "items": {
            "type": "object",
            "properties": {
              "ejercicio": {
                "type": "string",
                "title": "Nombre del ejercicio"
              },
              "objetivo": {
                "type": "string",
                "title": "Objetivo específico"
              },
              "frecuenciaDiaria": {
                "type": "string",
                "title": "Frecuencia diaria recomendada"
              }
            }
          }
        }
      },
      "required": ["objetivosTerapia", "frecuenciaSesiones"]
    },
    "estructuraInversión": {
      "title": "ESTRUCTURA DE INVERSIÓN",
      "type": "object",
      "properties": {
        "paqueteSeleccionado": {
          "type": "object",
          "title": "Paquete seleccionado",
          "properties": {
            "tipoPaquete": {
              "type": "string",
              "title": "Tipo de paquete",
              "enum": [
                "Básico (10 sesiones)",
                "Estándar (20 sesiones)",
                "Completo (30 sesiones)",
                "Personalizado"
              ]
            },
            "sesionesIncluidas": {
              "type": "integer",
              "title": "Sesiones incluidas",
              "minimum": 1
            },
            "costoPaquete": {
              "type": "number",
              "title": "Costo del paquete"
            },
            "validez": {
              "type": "integer",
              "title": "Validez (meses)",
              "minimum": 1,
              "maximum": 12
            }
          }
        },
        "opcionesPago": {
          "type": "object",
          "title": "Opciones de pago",
          "properties": {
            "pagoCompleto": {
              "type": "object",
              "title": "Pago completo",
              "properties": {
                "monto": {
                  "type": "number",
                  "title": "Monto"
                },
                "descuento": {
                  "type": "number",
                  "title": "Descuento por pago completo (%)"
                }
              }
            },
            "pagoParcial": {
              "type": "object",
              "title": "Pago parcial",
              "properties": {
                "cuotaInicial": {
                  "type": "number",
                  "title": "Cuota inicial (%)"
                },
                "cuotasRestantes": {
                  "type": "integer",
                  "title": "Número de cuotas"
                }
              }
            },
            "pagoPorSesion": {
              "type": "object",
              "title": "Pago por sesión",
              "properties": {
                "costoSesion": {
                  "type": "number",
                  "title": "Costo por sesión individual"
                },
                "minimoSesiones": {
                  "type": "integer",
                  "title": "Mínimo de sesiones comprometidas"
                }
              }
            }
          }
        },
        "materialesIncluidos": {
          "type": "array",
          "title": "Materiales incluidos",
          "items": {
            "type": "string",
            "enum": [
              "Guía de ejercicios impresa",
              "Acceso a plataforma digital",
              "Videos instructivos",
              "Materiales de estimulación",
              "Registro de progreso",
              "Informes periódicos"
            ]
          }
        }
      },
      "required": ["paqueteSeleccionado", "opcionesPago"]
    },
    "seguimientoProgreso": {
      "title": "SEGUIMIENTO DEL PROGRESO",
      "type": "object",
      "properties": {
        "registroSesiones": {
          "type": "object",
          "title": "Registro de sesiones",
          "properties": {
            "fichaSeguimiento": {
              "type": "boolean",
              "title": "¿Se llevará ficha de seguimiento?"
            },
            "evaluacionesPeriódicas": {
              "type": "integer",
              "title": "Evaluaciones periódicas cada (sesiones)",
              "minimum": 1,
              "maximum": 20
            },
            "registroFotográfico": {
              "type": "boolean",
              "title": "¿Registro fotográfico del progreso?"
            }
          }
        },
        "indicadoresProgreso": {
          "type": "array",
          "title": "Indicadores de progreso a monitorear",
          "items": {
            "type": "string",
            "enum": [
              "Fuerza muscular",
              "Coordinación",
              "Postura labial en reposo",
              "Posición lingual",
              "Patrón de deglución",
              "Respiración nasal",
              "Consistencia en ejercicios",
              "Transferencia a vida diaria"
            ]
          }
        },
        "comunicacionFamilia": {
          "type": "object",
          "title": "Comunicación con la familia",
          "properties": {
            "reunionesSeguimiento": {
              "type": "integer",
              "title": "Reuniones de seguimiento cada (sesiones)",
              "minimum": 1,
              "maximum": 10
            },
            "reportesProgreso": {
              "type": "boolean",
              "title": "¿Reportes escritos de progreso?"
            },
            "canalComunicacion": {
              "type": "string",
              "title": "Canal principal de comunicación",
              "enum": ["WhatsApp", "Email", "Teléfono", "Plataforma"]
            }
          }
        }
      }
    },
    "compromisosPartes": {
      "title": "COMPROMISOS DE LAS PARTES",
      "type": "object",
      "properties": {
        "compromisoPaciente": {
          "type": "object",
          "title": "Compromiso del paciente/familia",
          "properties": {
            "asistenciaPuntual": {
              "type": "boolean",
              "title": "Asistencia puntual a sesiones"
            },
            "ejerciciosDiarios": {
              "type": "boolean",
              "title": "Realización de ejercicios diarios"
            },
            "registroProgreso": {
              "type": "boolean",
              "title": "Registro diario de práctica"
            },
            "comunicacionCambios": {
              "type": "boolean",
              "title": "Comunicación de cambios o problemas"
            }
          }
        },
        "compromisoTerapeuta": {
          "type": "object",
          "title": "Compromiso del terapeuta",
          "properties": {
            "preparacionSesiones": {
              "type": "boolean",
              "title": "Preparación adecuada de sesiones"
            },
            "actualizacionPlan": {
              "type": "boolean",
              "title": "Actualización del plan según progreso"
            },
            "disponibilidadConsulta": {
              "type": "boolean",
              "title": "Disponibilidad para consultas entre sesiones"
            },
            "confidencialidad": {
              "type": "boolean",
              "title": "Mantenimiento de confidencialidad"
            }
          }
        },
        "acuerdosEspeciales": {
          "type": "array",
          "title": "Acuerdos especiales",
          "items": {
            "type": "object",
            "properties": {
              "acuerdo": {
                "type": "string",
                "title": "Descripción del acuerdo"
              },
              "responsable": {
                "type": "string",
                "title": "Responsable"
              }
            }
          }
        }
      },
      "required": ["compromisoPaciente", "compromisoTerapeuta"]
    }
  },
  "required": [
    "evaluacionInicial",
    "planTerapeutico",
    "estructuraInversión",
    "compromisosPartes"
  ]
}', 0, NOW(), 'Seeds');
