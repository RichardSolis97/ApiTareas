PGDMP     '                
    z            dbTareas    12.12    15.0                0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false                       0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false                       0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false                       1262    32953    dbTareas    DATABASE     ?   CREATE DATABASE "dbTareas" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Spanish_Latin America.1252';
    DROP DATABASE "dbTareas";
                postgres    false                        2615    2200    public    SCHEMA     2   -- *not* creating schema, since initdb creates it
 2   -- *not* dropping schema, since initdb creates it
                postgres    false                       0    0    SCHEMA public    ACL     Q   REVOKE USAGE ON SCHEMA public FROM PUBLIC;
GRANT ALL ON SCHEMA public TO PUBLIC;
                   postgres    false    6            ?            1259    32967    Tb_Colaborador    TABLE     g   CREATE TABLE public."Tb_Colaborador" (
    "Id" integer NOT NULL,
    colaborador character varying
);
 $   DROP TABLE public."Tb_Colaborador";
       public         heap    postgres    false    6            ?            1259    32965    Tb_Colaborador_Id_seq    SEQUENCE     ?   CREATE SEQUENCE public."Tb_Colaborador_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 .   DROP SEQUENCE public."Tb_Colaborador_Id_seq";
       public          postgres    false    203    6                       0    0    Tb_Colaborador_Id_seq    SEQUENCE OWNED BY     U   ALTER SEQUENCE public."Tb_Colaborador_Id_seq" OWNED BY public."Tb_Colaborador"."Id";
          public          postgres    false    202            ?            1259    32983 	   Tb_Tareas    TABLE     :  CREATE TABLE public."Tb_Tareas" (
    "Id" integer NOT NULL,
    "Descripcion" character varying(200),
    "Colaborador" integer,
    "Estado" character varying(50),
    "Prioridad" character varying(50),
    "Notas" character varying(200),
    "FechaInicio" character varying,
    "FechaFin" character varying
);
    DROP TABLE public."Tb_Tareas";
       public         heap    postgres    false    6            ?            1259    32981    Tb_Tareas_Id_seq    SEQUENCE     ?   CREATE SEQUENCE public."Tb_Tareas_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 )   DROP SEQUENCE public."Tb_Tareas_Id_seq";
       public          postgres    false    205    6                       0    0    Tb_Tareas_Id_seq    SEQUENCE OWNED BY     K   ALTER SEQUENCE public."Tb_Tareas_Id_seq" OWNED BY public."Tb_Tareas"."Id";
          public          postgres    false    204            ?
           2604    32970    Tb_Colaborador Id    DEFAULT     |   ALTER TABLE ONLY public."Tb_Colaborador" ALTER COLUMN "Id" SET DEFAULT nextval('public."Tb_Colaborador_Id_seq"'::regclass);
 D   ALTER TABLE public."Tb_Colaborador" ALTER COLUMN "Id" DROP DEFAULT;
       public          postgres    false    202    203    203            ?
           2604    32986    Tb_Tareas Id    DEFAULT     r   ALTER TABLE ONLY public."Tb_Tareas" ALTER COLUMN "Id" SET DEFAULT nextval('public."Tb_Tareas_Id_seq"'::regclass);
 ?   ALTER TABLE public."Tb_Tareas" ALTER COLUMN "Id" DROP DEFAULT;
       public          postgres    false    205    204    205                      0    32967    Tb_Colaborador 
   TABLE DATA           =   COPY public."Tb_Colaborador" ("Id", colaborador) FROM stdin;
    public          postgres    false    203   ?                 0    32983 	   Tb_Tareas 
   TABLE DATA           ?   COPY public."Tb_Tareas" ("Id", "Descripcion", "Colaborador", "Estado", "Prioridad", "Notas", "FechaInicio", "FechaFin") FROM stdin;
    public          postgres    false    205   ?                  0    0    Tb_Colaborador_Id_seq    SEQUENCE SET     E   SELECT pg_catalog.setval('public."Tb_Colaborador_Id_seq"', 3, true);
          public          postgres    false    202                       0    0    Tb_Tareas_Id_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('public."Tb_Tareas_Id_seq"', 3, true);
          public          postgres    false    204            ?
           2606    32975    Tb_Colaborador Pk_colaborador 
   CONSTRAINT     a   ALTER TABLE ONLY public."Tb_Colaborador"
    ADD CONSTRAINT "Pk_colaborador" PRIMARY KEY ("Id");
 K   ALTER TABLE ONLY public."Tb_Colaborador" DROP CONSTRAINT "Pk_colaborador";
       public            postgres    false    203            ?
           2606    32991    Tb_Tareas pk_tareas 
   CONSTRAINT     U   ALTER TABLE ONLY public."Tb_Tareas"
    ADD CONSTRAINT pk_tareas PRIMARY KEY ("Id");
 ?   ALTER TABLE ONLY public."Tb_Tareas" DROP CONSTRAINT pk_tareas;
       public            postgres    false    205            ?
           2606    32992    Tb_Tareas fk_colaborador    FK CONSTRAINT     ?   ALTER TABLE ONLY public."Tb_Tareas"
    ADD CONSTRAINT fk_colaborador FOREIGN KEY ("Colaborador") REFERENCES public."Tb_Colaborador"("Id");
 D   ALTER TABLE ONLY public."Tb_Tareas" DROP CONSTRAINT fk_colaborador;
       public          postgres    false    2698    203    205                   x?3??L?H,J?2?t???+??????? V?[         |   x?3?(*MMJ4?4?t??K?ɬJLI?t?)I?????Y?Y 	)?ĸ???pe??g?`hbed?l??	\????hV@j^Jfj^I*?ojJf"\aVHF??=... ??7v     