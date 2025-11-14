-- ===============================
-- Seed inicial de Admin y SuperUser
-- ===============================

-- Insertar persona
INSERT INTO public.person 
("person_id", "person_name", "person_last_name", "birth_date", "sex", "ci", "email", "phone_number", "profession", "state", "created_at", "created_by")
VALUES 
('13a8cd23-5185-4f42-8335-9eaadfc17fae', 'Admin', 'Super', '2003-01-18', 0, '0000000', 'admin@super.com', '123123123', 'Admin', 0, NOW(), 'Seeds')
ON CONFLICT ("person_id") DO NOTHING;

-- Insertar persona dentista
INSERT INTO public.person 
("person_id", "person_name", "person_last_name", "birth_date", "sex", "ci", "email", "phone_number", "profession", "state", "created_at", "created_by")
VALUES 
('8832dc7d-710f-45da-b147-aaa8b3b2f977', 'Dentista', '1', '1968-06-23', 0, '1111111', 'dentista@email.com', '12411242', 'Odontólogo', 0, NOW(), 'Seeds')
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
('aca377cb-7c24-41b1-acf5-6586093961c0', '8c0c77a5-3ca1-482c-b078-750a9b82ad5e', null, 'Calle 1', 'Zona 1', 'Ciudad 1', '12345678', 'Estudiante', 'Universidad', '000001020', '', 0, 'Seeds', NOW())
ON CONFLICT (patient_id) DO NOTHING;


-- ===============================
-- Insertar cita
-- ===============================
INSERT INTO public.appointment
("appointment_id", "patient_id", "professional_id", "start_date", "end_date", "appointment_type", "status", "reason", "observations", "state", "created_by", "created_at")
VALUES
('eca377cb-7c24-41b1-acf5-6586093961c0', 'aca377cb-7c24-41b1-acf5-6586093961c0', '01429580-7e2d-43b5-ae88-93229119c048', '2025-10-29T12:25:00', '2025-10-29T12:50:00', 'Emergencia', 'Confirmado', 'Ejemplo de razón', 'Ejemplo de observación...', 0, 'Seeds', NOW());


-- ===============================
-- Insertar permisos
-- ===============================
INSERT INTO public.permission ("permission_id", "permission_name", "permission_description", "state", "created_at", "created_by")
VALUES
    ('11111111-1111-1111-1111-111111111111', 'Create.User', 'Permite crear usuarios', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111112', 'Read.User', 'Permite leer usuarios', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111113', 'Update.User', 'Permite actualizar usuarios', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111114', 'Delete.User', 'Permite eliminar usuarios', 0, NOW(), 'Seeds'),

    ('11111111-1111-1111-1111-111111111115', 'Create.Role', 'Permite crear roles', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111116', 'Read.Role', 'Permite leer roles', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111117', 'Update.Role', 'Permite actualizar roles', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111118', 'Delete.Role', 'Permite eliminar roles', 0, NOW(), 'Seeds'),

    ('11111111-1111-1111-1111-111111111119', 'Create.Permission', 'Permite crear permisos', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111120', 'Read.Permission', 'Permite leer permisos', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111121', 'Update.Permission', 'Permite actualizar permisos', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111122', 'Delete.Permission', 'Permite eliminar permisos', 0, NOW(), 'Seeds'),

    ('11111111-1111-1111-1111-111111111123', 'Create.RolePermission', 'Permite crear permisos de roles', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111124', 'Read.RolePermission', 'Permite leer permisos de roles', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111125', 'Update.RolePermission', 'Permite actualizar permisos de roles', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111126', 'Delete.RolePermission', 'Permite eliminar permisos de roles', 0, NOW(), 'Seeds'),

    ('11111111-1111-1111-1111-111111111127', 'Create.Clinic', 'Permite crear clinicas', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111128', 'Read.Clinic', 'Permite leer clinicas', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111129', 'Update.Clinic', 'Permite actualizar clinicas', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111130', 'Delete.Clinic', 'Permite eliminar clinicas', 0, NOW(), 'Seeds'),

    ('11111111-1111-1111-1111-111111111131', 'Create.Patient', 'Permite crear pacientes', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111132', 'Read.Patient', 'Permite leer pacientes', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111133', 'Update.Patient', 'Permite actualizar pacientes', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111134', 'Delete.Patient', 'Permite eliminar pacientes', 0, NOW(), 'Seeds'),

    ('11111111-1111-1111-1111-111111111135', 'Create.Appointment', 'Permite crear pacientes', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111136', 'Read.Appointment', 'Permite leer pacientes', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111137', 'Update.Appointment', 'Permite actualizar pacientes', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111138', 'Delete.Appointment', 'Permite eliminar pacientes', 0, NOW(), 'Seeds'),

    ('11111111-1111-1111-1111-111111111139', 'Create.EvidenceFile', 'Permite crear archivos', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111140', 'Read.EvidenceFile', 'Permite leer archivos', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111141', 'Update.EvidenceFile', 'Permite actualizar archivos', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111142', 'Delete.EvidenceFile', 'Permite eliminar archivos', 0, NOW(), 'Seeds'),

    ('11111111-1111-1111-1111-111111111143', 'Create.Monitoring', 'Permite crear seguimientos', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111144', 'Read.Monitoring', 'Permite leer seguimientos', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111145', 'Update.Monitoring', 'Permite actualizar seguimientos', 0, NOW(), 'Seeds'),
    ('11111111-1111-1111-1111-111111111146', 'Delete.Monitoring', 'Permite eliminar seguimientos', 0, NOW(), 'Seeds');

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
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111123', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111124', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111125', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111126', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111127', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111128', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111129', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111130', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111131', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111132', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111133', 0, NOW(), 'Seeds'),
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111134', 0, NOW(), 'Seeds'),
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
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111146', 0, NOW(), 'Seeds');