-- ===============================
-- Seed inicial de Admin y SuperUser
-- ===============================

-- Insertar persona
INSERT INTO public.person 
("person_id", "person_name", "person_last_name", "birth_date", "sex", "ci", "email", "phone_number", "profession", "state", "created_at", "created_by")
VALUES 
('a4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', 'Admin', 'Super', '2003-01-18', 0, '0000000', 'admin@super.com', '123123123', 'Admin', 0, NOW(), 'Seeds')
ON CONFLICT ("person_id") DO NOTHING;

-- Insertar usuario asociado
INSERT INTO public.user
("user_id", "person_id", "password", "state", "created_at", "created_by")
VALUES
('c4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', 'a4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5',
'$2a$16$JCCmzFrerVGNZLSaL9Cqf.ntKalIpWxtWj2SJISKGP/pgqC1IQrvC', -- Hash bcrypt de "123456"
0, '2021-01-18', 'Seeds')
ON CONFLICT ("user_id") DO NOTHING;

-- Crear rol Admin si no existe
INSERT INTO public.role ("role_id", "role_name", "role_description", "state", "created_at", "created_by")
VALUES ('d4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', 'Admin', 'Rol administrador', 0, NOW(), 'Seeds')
ON CONFLICT ("role_id") DO NOTHING;

-- Asignar rol al usuario
INSERT INTO public.user_rol ("user_rol_id", "user_id", "role_id", "state", "created_at", "created_by")
VALUES ('06bf85b1-08df-45a6-8607-5626a4045d7a', 'c4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', 0, NOW(), 'Seeds')
ON CONFLICT DO NOTHING;

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
    ('11111111-1111-1111-1111-111111111134', 'Delete.Patient', 'Permite eliminar pacientes', 0, NOW(), 'Seeds');

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
    (gen_random_uuid(), 'd4f1f8a0-f7a9-4b8c-a7e1-e9e0a2e3e4e5', '11111111-1111-1111-1111-111111111134', 0, NOW(), 'Seeds');