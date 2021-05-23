-- Generated by Oracle SQL Developer Data Modeler 21.1.0.092.1221
--   at:        2021-05-23 16:01:10 EEST
--   site:      Oracle Database 11g
--   type:      Oracle Database 11g



-- predefined type, no DDL - MDSYS.SDO_GEOMETRY

-- predefined type, no DDL - XMLTYPE

CREATE TABLE application_settings (
    users_user_id  NUMBER(11) NOT NULL,
    hour_format    VARCHAR2(20) DEFAULT 'hh:mm tt' NOT NULL,
    date_format    VARCHAR2(20) DEFAULT 'MM/dd/yyyy' NOT NULL
);

ALTER TABLE application_settings
    ADD CONSTRAINT hour_format_ck CHECK ( hour_format IN ( 'HH:mm', 'hh:mm tt' ) );

ALTER TABLE application_settings
    ADD CONSTRAINT date_format_ck CHECK ( date_format IN ( 'MM/dd/yyyy', 'MMMM/dd/yyyy', 'dd/MM/yyyy', 'dd/MMMM/yyyy' ) );

ALTER TABLE application_settings ADD CONSTRAINT application_settings_pk PRIMARY KEY ( users_user_id );

CREATE TABLE conversations (
    conversation_id  NUMBER(13) NOT NULL,
    users_user_id    NUMBER(11) NOT NULL,
    users_user_id2   NUMBER(11) NOT NULL
);

ALTER TABLE conversations ADD CONSTRAINT conversations_pk PRIMARY KEY ( conversation_id );

ALTER TABLE conversations ADD CONSTRAINT conversations_un UNIQUE ( users_user_id2,
                                                                   users_user_id );

CREATE TABLE friend_relationships (
    relationship_id  NUMBER(13) NOT NULL,
    users_user_id    NUMBER(11) NOT NULL,
    users_user_id2   NUMBER(11) NOT NULL,
    status           VARCHAR2(10) DEFAULT 'pending' NOT NULL
);

ALTER TABLE friend_relationships
    ADD CONSTRAINT friend_status_ck CHECK ( status IN ( 'friends', 'pending' ) );

ALTER TABLE friend_relationships ADD CONSTRAINT friend_relationships_pk PRIMARY KEY ( relationship_id );

ALTER TABLE friend_relationships ADD CONSTRAINT friend_relationships_un UNIQUE ( users_user_id,
                                                                                 users_user_id2 );

CREATE TABLE messages (
    message_id                     NUMBER(14) NOT NULL,
    format                         VARCHAR2(4) NOT NULL,
    message_data                   BLOB NOT NULL,
    sent_at                        DATE NOT NULL,
    users_user_id                  NUMBER(11) NOT NULL,
    conversations_conversation_id  NUMBER(13) NOT NULL
);

ALTER TABLE messages ADD CONSTRAINT messages_pk PRIMARY KEY ( message_id );

CREATE TABLE relationship_settings (
    nickname_user_1          VARCHAR2(30),
    nickname_user_2          VARCHAR2(30),
    friend_relationships_id  NUMBER(13) NOT NULL
);

ALTER TABLE relationship_settings ADD CONSTRAINT relationship_settings_pk PRIMARY KEY ( friend_relationships_id );

CREATE TABLE user_informations (
    first_name     VARCHAR2(40) NOT NULL,
    last_name      VARCHAR2(40) NOT NULL,
    email          VARCHAR2(60) NOT NULL,
    birthdate      DATE NOT NULL,
    users_user_id  NUMBER(11) NOT NULL
);

ALTER TABLE user_informations
    ADD CONSTRAINT email_ck CHECK ( REGEXP_LIKE ( email,
                                                  '[a-z0-9._%-]+@[a-z0-9._%-]+\.[a-z]{2,4}' ) );

ALTER TABLE user_informations ADD CONSTRAINT user_informations_pk PRIMARY KEY ( users_user_id );

CREATE TABLE users (
    user_id        NUMBER(11) NOT NULL,
    user_name      VARCHAR2(40) NOT NULL,
    user_password  VARCHAR2(64) NOT NULL,
    created_at     DATE DEFAULT current_date NOT NULL
);

ALTER TABLE users
    ADD CONSTRAINT user_name_ck CHECK ( REGEXP_LIKE ( user_name,
                                                      '^\w{8,16}$' ) );

ALTER TABLE users
    ADD CONSTRAINT user_password_ck CHECK ( REGEXP_LIKE ( user_password,
                                                          '\S{8,16}$' ) );

ALTER TABLE users ADD CONSTRAINT users_pk PRIMARY KEY ( user_id );

ALTER TABLE users ADD CONSTRAINT users_user_name_un UNIQUE ( user_name );

ALTER TABLE application_settings
    ADD CONSTRAINT application_settings_users_fk FOREIGN KEY ( users_user_id )
        REFERENCES users ( user_id )
            ON DELETE CASCADE;

ALTER TABLE conversations
    ADD CONSTRAINT conversations_users_fk FOREIGN KEY ( users_user_id )
        REFERENCES users ( user_id )
            ON DELETE CASCADE;

ALTER TABLE conversations
    ADD CONSTRAINT conversations_users_fkv2 FOREIGN KEY ( users_user_id2 )
        REFERENCES users ( user_id )
            ON DELETE CASCADE;

ALTER TABLE friend_relationships
    ADD CONSTRAINT friend_relationships_users_fk FOREIGN KEY ( users_user_id )
        REFERENCES users ( user_id )
            ON DELETE CASCADE;

ALTER TABLE friend_relationships
    ADD CONSTRAINT friend_relationships_users_fk2 FOREIGN KEY ( users_user_id2 )
        REFERENCES users ( user_id )
            ON DELETE CASCADE;

ALTER TABLE messages
    ADD CONSTRAINT messages_conversations_fk FOREIGN KEY ( conversations_conversation_id )
        REFERENCES conversations ( conversation_id )
            ON DELETE CASCADE;

ALTER TABLE messages
    ADD CONSTRAINT messages_users_fk FOREIGN KEY ( users_user_id )
        REFERENCES users ( user_id )
            ON DELETE CASCADE;

ALTER TABLE relationship_settings
    ADD CONSTRAINT relationships_id_fk FOREIGN KEY ( friend_relationships_id )
        REFERENCES friend_relationships ( relationship_id )
            ON DELETE CASCADE;

ALTER TABLE user_informations
    ADD CONSTRAINT user_informations_users_fk FOREIGN KEY ( users_user_id )
        REFERENCES users ( user_id )
            ON DELETE CASCADE;

CREATE SEQUENCE conversations_conversation_id START WITH 1 NOCACHE ORDER;

CREATE OR REPLACE TRIGGER conversations_conversation_id BEFORE
    INSERT ON conversations
    FOR EACH ROW
    WHEN ( new.conversation_id IS NULL )
BEGIN
    :new.conversation_id := conversations_conversation_id.nextval;
END;
/

CREATE SEQUENCE friend_relationships_relations START WITH 1 NOCACHE ORDER;

CREATE OR REPLACE TRIGGER friend_relationships_relations BEFORE
    INSERT ON friend_relationships
    FOR EACH ROW
    WHEN ( new.relationship_id IS NULL )
BEGIN
    :new.relationship_id := friend_relationships_relations.nextval;
END;
/

CREATE SEQUENCE messages_message_id_seq START WITH 1 NOCACHE ORDER;

CREATE OR REPLACE TRIGGER messages_message_id_trg BEFORE
    INSERT ON messages
    FOR EACH ROW
    WHEN ( new.message_id IS NULL )
BEGIN
    :new.message_id := messages_message_id_seq.nextval;
END;
/

CREATE SEQUENCE users_user_id_seq START WITH 1 NOCACHE ORDER;

CREATE OR REPLACE TRIGGER users_user_id_trg BEFORE
    INSERT ON users
    FOR EACH ROW
    WHEN ( new.user_id IS NULL )
BEGIN
    :new.user_id := users_user_id_seq.nextval;
END;
/



-- Oracle SQL Developer Data Modeler Summary Report: 
-- 
-- CREATE TABLE                             7
-- CREATE INDEX                             0
-- ALTER TABLE                             25
-- CREATE VIEW                              0
-- ALTER VIEW                               0
-- CREATE PACKAGE                           0
-- CREATE PACKAGE BODY                      0
-- CREATE PROCEDURE                         0
-- CREATE FUNCTION                          0
-- CREATE TRIGGER                           4
-- ALTER TRIGGER                            0
-- CREATE COLLECTION TYPE                   0
-- CREATE STRUCTURED TYPE                   0
-- CREATE STRUCTURED TYPE BODY              0
-- CREATE CLUSTER                           0
-- CREATE CONTEXT                           0
-- CREATE DATABASE                          0
-- CREATE DIMENSION                         0
-- CREATE DIRECTORY                         0
-- CREATE DISK GROUP                        0
-- CREATE ROLE                              0
-- CREATE ROLLBACK SEGMENT                  0
-- CREATE SEQUENCE                          4
-- CREATE MATERIALIZED VIEW                 0
-- CREATE MATERIALIZED VIEW LOG             0
-- CREATE SYNONYM                           0
-- CREATE TABLESPACE                        0
-- CREATE USER                              0
-- 
-- DROP TABLESPACE                          0
-- DROP DATABASE                            0
-- 
-- REDACTION POLICY                         0
-- 
-- ORDS DROP SCHEMA                         0
-- ORDS ENABLE SCHEMA                       0
-- ORDS ENABLE OBJECT                       0
-- 
-- ERRORS                                   0
-- WARNINGS                                 0
