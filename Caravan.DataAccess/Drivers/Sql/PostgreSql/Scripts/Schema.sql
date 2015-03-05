--
-- PostgreSQL database dump
--

-- Dumped from database version 9.4.1
-- Dumped by pg_dump version 9.4.1
-- Started on 2015-03-01 23:44:50

SET statement_timeout = 0;
SET lock_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;

--
-- TOC entry 7 (class 2615 OID 19015)
-- Name: caravan; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA caravan;


ALTER SCHEMA caravan OWNER TO postgres;

--
-- TOC entry 193 (class 3079 OID 11855)
-- Name: plpgsql; Type: EXTENSION; Schema: -; Owner: 
--

CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;


--
-- TOC entry 2158 (class 0 OID 0)
-- Dependencies: 193
-- Name: EXTENSION plpgsql; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';


SET search_path = caravan, pg_catalog;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- TOC entry 174 (class 1259 OID 19018)
-- Name: CRVN_LOG_ENTRIES; Type: TABLE; Schema: caravan; Owner: postgres; Tablespace: 
--

CREATE TABLE "CRVN_LOG_ENTRIES" (
    "CLOG_ID" bigint NOT NULL,
    "CAPP_ID" integer DEFAULT 0 NOT NULL,
    "CLOS_TYPE" character varying(8) DEFAULT ''::character varying NOT NULL,
    "CLOG_DATE" timestamp without time zone DEFAULT '-infinity'::timestamp without time zone NOT NULL,
    "CUSR_LOGIN" character varying(32),
    "CLOG_CODE_UNIT" character varying(256),
    "CLOG_FUNCTION" character varying(256),
    "CLOG_SHORT_MSG" character varying(256) DEFAULT ''::character varying NOT NULL,
    "CLOG_CONTEXT" character varying(256),
    "CLOG_KEY_0" character varying(32),
    "CLOG_VALUE_0" character varying(1024),
    "CLOG_KEY_1" character varying(32),
    "CLOG_VALUE_1" character varying(1024),
    "CLOG_KEY_2" character varying(32),
    "CLOG_VALUE_2" character varying(1024),
    "CLOG_KEY_3" character varying(32),
    "CLOG_VALUE_3" character varying(1024),
    "CLOG_KEY_4" character varying(32),
    "CLOG_VALUE_4" character varying(1024),
    "CLOG_KEY_5" character varying(32),
    "CLOG_VALUE_5" character varying(1024),
    "CLOG_KEY_6" character varying(32),
    "CLOG_VALUE_6" character varying(1024),
    "CLOG_KEY_7" character varying(32),
    "CLOG_VALUE_7" character varying(1024),
    "CLOG_KEY_8" character varying(32),
    "CLOG_VALUE_8" character varying(1024),
    "CLOG_KEY_9" character varying(32),
    "CLOG_VALUE_9" character varying(1024),
    "CLOG_LONG_MSG" text
);


ALTER TABLE "CRVN_LOG_ENTRIES" OWNER TO postgres;

--
-- TOC entry 173 (class 1259 OID 19016)
-- Name: CRVN_LOG_ENTRIES_CLOG_ID_seq; Type: SEQUENCE; Schema: caravan; Owner: postgres
--

CREATE SEQUENCE "CRVN_LOG_ENTRIES_CLOG_ID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE "CRVN_LOG_ENTRIES_CLOG_ID_seq" OWNER TO postgres;

--
-- TOC entry 2159 (class 0 OID 0)
-- Dependencies: 173
-- Name: CRVN_LOG_ENTRIES_CLOG_ID_seq; Type: SEQUENCE OWNED BY; Schema: caravan; Owner: postgres
--

ALTER SEQUENCE "CRVN_LOG_ENTRIES_CLOG_ID_seq" OWNED BY "CRVN_LOG_ENTRIES"."CLOG_ID";


--
-- TOC entry 189 (class 1259 OID 19113)
-- Name: CRVN_LOG_SETTINGS; Type: TABLE; Schema: caravan; Owner: postgres; Tablespace: 
--

CREATE TABLE "CRVN_LOG_SETTINGS" (
    "CAPP_ID" integer DEFAULT 0 NOT NULL,
    "CLOS_TYPE" character varying(8) DEFAULT ''::character varying NOT NULL,
    "CLOS_ENABLED" boolean DEFAULT false NOT NULL,
    "CLOS_DAYS" smallint DEFAULT 0 NOT NULL,
    "CLOS_MAX_ENTRIES" integer DEFAULT 0 NOT NULL
);


ALTER TABLE "CRVN_LOG_SETTINGS" OWNER TO postgres;

--
-- TOC entry 176 (class 1259 OID 19033)
-- Name: CRVN_SEC_APPS; Type: TABLE; Schema: caravan; Owner: postgres; Tablespace: 
--

CREATE TABLE "CRVN_SEC_APPS" (
    "CAPP_ID" integer NOT NULL,
    "CAPP_NAME" character varying(32) DEFAULT ''::character varying NOT NULL,
    "CAPP_DESCR" character varying(256)
);


ALTER TABLE "CRVN_SEC_APPS" OWNER TO postgres;

--
-- TOC entry 175 (class 1259 OID 19031)
-- Name: CRVN_SEC_APPS_CAPP_ID_seq; Type: SEQUENCE; Schema: caravan; Owner: postgres
--

CREATE SEQUENCE "CRVN_SEC_APPS_CAPP_ID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE "CRVN_SEC_APPS_CAPP_ID_seq" OWNER TO postgres;

--
-- TOC entry 2160 (class 0 OID 0)
-- Dependencies: 175
-- Name: CRVN_SEC_APPS_CAPP_ID_seq; Type: SEQUENCE OWNED BY; Schema: caravan; Owner: postgres
--

ALTER SEQUENCE "CRVN_SEC_APPS_CAPP_ID_seq" OWNED BY "CRVN_SEC_APPS"."CAPP_ID";


--
-- TOC entry 178 (class 1259 OID 19042)
-- Name: CRVN_SEC_CONTEXTS; Type: TABLE; Schema: caravan; Owner: postgres; Tablespace: 
--

CREATE TABLE "CRVN_SEC_CONTEXTS" (
    "CCTX_ID" integer NOT NULL,
    "CAPP_ID" integer DEFAULT 0 NOT NULL,
    "CCTX_NAME" character varying(256) DEFAULT ''::character varying NOT NULL,
    "CCTX_DESCR" character varying(1024)
);


ALTER TABLE "CRVN_SEC_CONTEXTS" OWNER TO postgres;

--
-- TOC entry 177 (class 1259 OID 19040)
-- Name: CRVN_SEC_CONTEXTS_CCTX_ID_seq; Type: SEQUENCE; Schema: caravan; Owner: postgres
--

CREATE SEQUENCE "CRVN_SEC_CONTEXTS_CCTX_ID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE "CRVN_SEC_CONTEXTS_CCTX_ID_seq" OWNER TO postgres;

--
-- TOC entry 2161 (class 0 OID 0)
-- Dependencies: 177
-- Name: CRVN_SEC_CONTEXTS_CCTX_ID_seq; Type: SEQUENCE OWNED BY; Schema: caravan; Owner: postgres
--

ALTER SEQUENCE "CRVN_SEC_CONTEXTS_CCTX_ID_seq" OWNED BY "CRVN_SEC_CONTEXTS"."CCTX_ID";


--
-- TOC entry 182 (class 1259 OID 19066)
-- Name: CRVN_SEC_ENTRIES; Type: TABLE; Schema: caravan; Owner: postgres; Tablespace: 
--

CREATE TABLE "CRVN_SEC_ENTRIES" (
    "CSEC_ID" bigint NOT NULL,
    "COBJ_ID" integer DEFAULT 0 NOT NULL,
    "CUSR_ID" bigint,
    "CGRP_ID" integer,
    "CROL_ID" integer
);


ALTER TABLE "CRVN_SEC_ENTRIES" OWNER TO postgres;

--
-- TOC entry 181 (class 1259 OID 19064)
-- Name: CRVN_SEC_ENTRIES_CSEC_ID_seq; Type: SEQUENCE; Schema: caravan; Owner: postgres
--

CREATE SEQUENCE "CRVN_SEC_ENTRIES_CSEC_ID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE "CRVN_SEC_ENTRIES_CSEC_ID_seq" OWNER TO postgres;

--
-- TOC entry 2162 (class 0 OID 0)
-- Dependencies: 181
-- Name: CRVN_SEC_ENTRIES_CSEC_ID_seq; Type: SEQUENCE OWNED BY; Schema: caravan; Owner: postgres
--

ALTER SEQUENCE "CRVN_SEC_ENTRIES_CSEC_ID_seq" OWNED BY "CRVN_SEC_ENTRIES"."CSEC_ID";


--
-- TOC entry 184 (class 1259 OID 19075)
-- Name: CRVN_SEC_GROUPS; Type: TABLE; Schema: caravan; Owner: postgres; Tablespace: 
--

CREATE TABLE "CRVN_SEC_GROUPS" (
    "CGRP_ID" integer NOT NULL,
    "CAPP_ID" integer DEFAULT 0 NOT NULL,
    "CGRP_NAME" character varying(32) DEFAULT ''::character varying NOT NULL,
    "CGRP_DESCR" character varying(256),
    "CGRP_NOTES" character varying(1024)
);


ALTER TABLE "CRVN_SEC_GROUPS" OWNER TO postgres;

--
-- TOC entry 183 (class 1259 OID 19073)
-- Name: CRVN_SEC_GROUPS_CGRP_ID_seq; Type: SEQUENCE; Schema: caravan; Owner: postgres
--

CREATE SEQUENCE "CRVN_SEC_GROUPS_CGRP_ID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE "CRVN_SEC_GROUPS_CGRP_ID_seq" OWNER TO postgres;

--
-- TOC entry 2163 (class 0 OID 0)
-- Dependencies: 183
-- Name: CRVN_SEC_GROUPS_CGRP_ID_seq; Type: SEQUENCE OWNED BY; Schema: caravan; Owner: postgres
--

ALTER SEQUENCE "CRVN_SEC_GROUPS_CGRP_ID_seq" OWNED BY "CRVN_SEC_GROUPS"."CGRP_ID";


--
-- TOC entry 180 (class 1259 OID 19055)
-- Name: CRVN_SEC_OBJECTS; Type: TABLE; Schema: caravan; Owner: postgres; Tablespace: 
--

CREATE TABLE "CRVN_SEC_OBJECTS" (
    "COBJ_ID" integer NOT NULL,
    "CCTX_ID" integer DEFAULT 0 NOT NULL,
    "COBJ_NAME" character varying(32) DEFAULT ''::character varying NOT NULL,
    "COBJ_DESCR" character varying(256),
    "COBJ_TYPE" character varying(8) DEFAULT ''::character varying NOT NULL
);


ALTER TABLE "CRVN_SEC_OBJECTS" OWNER TO postgres;

--
-- TOC entry 179 (class 1259 OID 19053)
-- Name: CRVN_SEC_OBJECTS_COBJ_ID_seq; Type: SEQUENCE; Schema: caravan; Owner: postgres
--

CREATE SEQUENCE "CRVN_SEC_OBJECTS_COBJ_ID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE "CRVN_SEC_OBJECTS_COBJ_ID_seq" OWNER TO postgres;

--
-- TOC entry 2164 (class 0 OID 0)
-- Dependencies: 179
-- Name: CRVN_SEC_OBJECTS_COBJ_ID_seq; Type: SEQUENCE OWNED BY; Schema: caravan; Owner: postgres
--

ALTER SEQUENCE "CRVN_SEC_OBJECTS_COBJ_ID_seq" OWNED BY "CRVN_SEC_OBJECTS"."COBJ_ID";


--
-- TOC entry 186 (class 1259 OID 19088)
-- Name: CRVN_SEC_ROLES; Type: TABLE; Schema: caravan; Owner: postgres; Tablespace: 
--

CREATE TABLE "CRVN_SEC_ROLES" (
    "CROL_ID" integer NOT NULL,
    "CGRP_ID" integer DEFAULT 0 NOT NULL,
    "CROL_NAME" character varying(32) DEFAULT ''::character varying NOT NULL,
    "CROL_DESCR" character varying(256),
    "CROL_NOTES" character varying(1024)
);


ALTER TABLE "CRVN_SEC_ROLES" OWNER TO postgres;

--
-- TOC entry 185 (class 1259 OID 19086)
-- Name: CRVN_SEC_ROLES_CROL_ID_seq; Type: SEQUENCE; Schema: caravan; Owner: postgres
--

CREATE SEQUENCE "CRVN_SEC_ROLES_CROL_ID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE "CRVN_SEC_ROLES_CROL_ID_seq" OWNER TO postgres;

--
-- TOC entry 2165 (class 0 OID 0)
-- Dependencies: 185
-- Name: CRVN_SEC_ROLES_CROL_ID_seq; Type: SEQUENCE OWNED BY; Schema: caravan; Owner: postgres
--

ALTER SEQUENCE "CRVN_SEC_ROLES_CROL_ID_seq" OWNED BY "CRVN_SEC_ROLES"."CROL_ID";


--
-- TOC entry 188 (class 1259 OID 19101)
-- Name: CRVN_SEC_USERS; Type: TABLE; Schema: caravan; Owner: postgres; Tablespace: 
--

CREATE TABLE "CRVN_SEC_USERS" (
    "CUSR_ID" bigint NOT NULL,
    "CAPP_ID" integer DEFAULT 0 NOT NULL,
    "CUSR_LOGIN" character varying(32) DEFAULT ''::character varying NOT NULL,
    "CUSR_HASHED_PWD" character varying(256),
    "CUSR_ACTIVE" boolean DEFAULT false NOT NULL,
    "CUSR_FIRST_NAME" character varying(256),
    "CUSR_LAST_NAME" character varying(256),
    "CUSR_EMAIL" character varying(256)
);


ALTER TABLE "CRVN_SEC_USERS" OWNER TO postgres;

--
-- TOC entry 187 (class 1259 OID 19099)
-- Name: CRVN_SEC_USERS_CUSR_ID_seq; Type: SEQUENCE; Schema: caravan; Owner: postgres
--

CREATE SEQUENCE "CRVN_SEC_USERS_CUSR_ID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE "CRVN_SEC_USERS_CUSR_ID_seq" OWNER TO postgres;

--
-- TOC entry 2166 (class 0 OID 0)
-- Dependencies: 187
-- Name: CRVN_SEC_USERS_CUSR_ID_seq; Type: SEQUENCE OWNED BY; Schema: caravan; Owner: postgres
--

ALTER SEQUENCE "CRVN_SEC_USERS_CUSR_ID_seq" OWNED BY "CRVN_SEC_USERS"."CUSR_ID";


--
-- TOC entry 191 (class 1259 OID 19130)
-- Name: CRVN_SEC_USER_GROUPS; Type: TABLE; Schema: caravan; Owner: postgres; Tablespace: 
--

CREATE TABLE "CRVN_SEC_USER_GROUPS" (
    "CGRP_ID" integer DEFAULT 0 NOT NULL,
    "CUSR_ID" bigint DEFAULT 0 NOT NULL
);


ALTER TABLE "CRVN_SEC_USER_GROUPS" OWNER TO postgres;

--
-- TOC entry 190 (class 1259 OID 19123)
-- Name: CRVN_SEC_USER_ROLES; Type: TABLE; Schema: caravan; Owner: postgres; Tablespace: 
--

CREATE TABLE "CRVN_SEC_USER_ROLES" (
    "CROL_ID" integer DEFAULT 0 NOT NULL,
    "CUSR_ID" bigint DEFAULT 0 NOT NULL
);


ALTER TABLE "CRVN_SEC_USER_ROLES" OWNER TO postgres;

--
-- TOC entry 192 (class 1259 OID 19234)
-- Name: __MigrationHistory; Type: TABLE; Schema: caravan; Owner: postgres; Tablespace: 
--

CREATE TABLE "__MigrationHistory" (
    "MigrationId" character varying(150) DEFAULT ''::character varying NOT NULL,
    "ContextKey" character varying(300) DEFAULT ''::character varying NOT NULL,
    "Model" bytea DEFAULT '\x'::bytea NOT NULL,
    "ProductVersion" character varying(32) DEFAULT ''::character varying NOT NULL
);


ALTER TABLE "__MigrationHistory" OWNER TO postgres;

--
-- TOC entry 1946 (class 2604 OID 19021)
-- Name: CLOG_ID; Type: DEFAULT; Schema: caravan; Owner: postgres
--

ALTER TABLE ONLY "CRVN_LOG_ENTRIES" ALTER COLUMN "CLOG_ID" SET DEFAULT nextval('"CRVN_LOG_ENTRIES_CLOG_ID_seq"'::regclass);


--
-- TOC entry 1951 (class 2604 OID 19036)
-- Name: CAPP_ID; Type: DEFAULT; Schema: caravan; Owner: postgres
--

ALTER TABLE ONLY "CRVN_SEC_APPS" ALTER COLUMN "CAPP_ID" SET DEFAULT nextval('"CRVN_SEC_APPS_CAPP_ID_seq"'::regclass);


--
-- TOC entry 1953 (class 2604 OID 19045)
-- Name: CCTX_ID; Type: DEFAULT; Schema: caravan; Owner: postgres
--

ALTER TABLE ONLY "CRVN_SEC_CONTEXTS" ALTER COLUMN "CCTX_ID" SET DEFAULT nextval('"CRVN_SEC_CONTEXTS_CCTX_ID_seq"'::regclass);


--
-- TOC entry 1960 (class 2604 OID 19069)
-- Name: CSEC_ID; Type: DEFAULT; Schema: caravan; Owner: postgres
--

ALTER TABLE ONLY "CRVN_SEC_ENTRIES" ALTER COLUMN "CSEC_ID" SET DEFAULT nextval('"CRVN_SEC_ENTRIES_CSEC_ID_seq"'::regclass);


--
-- TOC entry 1962 (class 2604 OID 19078)
-- Name: CGRP_ID; Type: DEFAULT; Schema: caravan; Owner: postgres
--

ALTER TABLE ONLY "CRVN_SEC_GROUPS" ALTER COLUMN "CGRP_ID" SET DEFAULT nextval('"CRVN_SEC_GROUPS_CGRP_ID_seq"'::regclass);


--
-- TOC entry 1956 (class 2604 OID 19058)
-- Name: COBJ_ID; Type: DEFAULT; Schema: caravan; Owner: postgres
--

ALTER TABLE ONLY "CRVN_SEC_OBJECTS" ALTER COLUMN "COBJ_ID" SET DEFAULT nextval('"CRVN_SEC_OBJECTS_COBJ_ID_seq"'::regclass);


--
-- TOC entry 1965 (class 2604 OID 19091)
-- Name: CROL_ID; Type: DEFAULT; Schema: caravan; Owner: postgres
--

ALTER TABLE ONLY "CRVN_SEC_ROLES" ALTER COLUMN "CROL_ID" SET DEFAULT nextval('"CRVN_SEC_ROLES_CROL_ID_seq"'::regclass);


--
-- TOC entry 1968 (class 2604 OID 19104)
-- Name: CUSR_ID; Type: DEFAULT; Schema: caravan; Owner: postgres
--

ALTER TABLE ONLY "CRVN_SEC_USERS" ALTER COLUMN "CUSR_ID" SET DEFAULT nextval('"CRVN_SEC_USERS_CUSR_ID_seq"'::regclass);


--
-- TOC entry 1988 (class 2606 OID 19030)
-- Name: PK_caravan.CRVN_LOG_ENTRIES; Type: CONSTRAINT; Schema: caravan; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "CRVN_LOG_ENTRIES"
    ADD CONSTRAINT "PK_caravan.CRVN_LOG_ENTRIES" PRIMARY KEY ("CLOG_ID");


--
-- TOC entry 2015 (class 2606 OID 19122)
-- Name: PK_caravan.CRVN_LOG_SETTINGS; Type: CONSTRAINT; Schema: caravan; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "CRVN_LOG_SETTINGS"
    ADD CONSTRAINT "PK_caravan.CRVN_LOG_SETTINGS" PRIMARY KEY ("CAPP_ID", "CLOS_TYPE");


--
-- TOC entry 1991 (class 2606 OID 19039)
-- Name: PK_caravan.CRVN_SEC_APPS; Type: CONSTRAINT; Schema: caravan; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "CRVN_SEC_APPS"
    ADD CONSTRAINT "PK_caravan.CRVN_SEC_APPS" PRIMARY KEY ("CAPP_ID");


--
-- TOC entry 1994 (class 2606 OID 19052)
-- Name: PK_caravan.CRVN_SEC_CONTEXTS; Type: CONSTRAINT; Schema: caravan; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "CRVN_SEC_CONTEXTS"
    ADD CONSTRAINT "PK_caravan.CRVN_SEC_CONTEXTS" PRIMARY KEY ("CCTX_ID");


--
-- TOC entry 2003 (class 2606 OID 19072)
-- Name: PK_caravan.CRVN_SEC_ENTRIES; Type: CONSTRAINT; Schema: caravan; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "CRVN_SEC_ENTRIES"
    ADD CONSTRAINT "PK_caravan.CRVN_SEC_ENTRIES" PRIMARY KEY ("CSEC_ID");


--
-- TOC entry 2006 (class 2606 OID 19085)
-- Name: PK_caravan.CRVN_SEC_GROUPS; Type: CONSTRAINT; Schema: caravan; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "CRVN_SEC_GROUPS"
    ADD CONSTRAINT "PK_caravan.CRVN_SEC_GROUPS" PRIMARY KEY ("CGRP_ID");


--
-- TOC entry 1997 (class 2606 OID 19063)
-- Name: PK_caravan.CRVN_SEC_OBJECTS; Type: CONSTRAINT; Schema: caravan; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "CRVN_SEC_OBJECTS"
    ADD CONSTRAINT "PK_caravan.CRVN_SEC_OBJECTS" PRIMARY KEY ("COBJ_ID");


--
-- TOC entry 2009 (class 2606 OID 19098)
-- Name: PK_caravan.CRVN_SEC_ROLES; Type: CONSTRAINT; Schema: caravan; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "CRVN_SEC_ROLES"
    ADD CONSTRAINT "PK_caravan.CRVN_SEC_ROLES" PRIMARY KEY ("CROL_ID");


--
-- TOC entry 2012 (class 2606 OID 19112)
-- Name: PK_caravan.CRVN_SEC_USERS; Type: CONSTRAINT; Schema: caravan; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "CRVN_SEC_USERS"
    ADD CONSTRAINT "PK_caravan.CRVN_SEC_USERS" PRIMARY KEY ("CUSR_ID");


--
-- TOC entry 2023 (class 2606 OID 19136)
-- Name: PK_caravan.CRVN_SEC_USER_GROUPS; Type: CONSTRAINT; Schema: caravan; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "CRVN_SEC_USER_GROUPS"
    ADD CONSTRAINT "PK_caravan.CRVN_SEC_USER_GROUPS" PRIMARY KEY ("CGRP_ID", "CUSR_ID");


--
-- TOC entry 2019 (class 2606 OID 19129)
-- Name: PK_caravan.CRVN_SEC_USER_ROLES; Type: CONSTRAINT; Schema: caravan; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "CRVN_SEC_USER_ROLES"
    ADD CONSTRAINT "PK_caravan.CRVN_SEC_USER_ROLES" PRIMARY KEY ("CROL_ID", "CUSR_ID");


--
-- TOC entry 2025 (class 2606 OID 19245)
-- Name: PK_caravan.__MigrationHistory; Type: CONSTRAINT; Schema: caravan; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "__MigrationHistory"
    ADD CONSTRAINT "PK_caravan.__MigrationHistory" PRIMARY KEY ("MigrationId", "ContextKey");


--
-- TOC entry 1985 (class 1259 OID 19137)
-- Name: CRVN_LOG_ENTRIES_IX_CRVN_LOG_DATE; Type: INDEX; Schema: caravan; Owner: postgres; Tablespace: 
--

CREATE INDEX "CRVN_LOG_ENTRIES_IX_CRVN_LOG_DATE" ON "CRVN_LOG_ENTRIES" USING btree ("CAPP_ID", "CLOG_DATE");


--
-- TOC entry 1986 (class 1259 OID 19138)
-- Name: CRVN_LOG_ENTRIES_IX_CRVN_LOG_TYPE; Type: INDEX; Schema: caravan; Owner: postgres; Tablespace: 
--

CREATE INDEX "CRVN_LOG_ENTRIES_IX_CRVN_LOG_TYPE" ON "CRVN_LOG_ENTRIES" USING btree ("CAPP_ID", "CLOS_TYPE");


--
-- TOC entry 2013 (class 1259 OID 19149)
-- Name: CRVN_LOG_SETTINGS_IX_CAPP_ID; Type: INDEX; Schema: caravan; Owner: postgres; Tablespace: 
--

CREATE INDEX "CRVN_LOG_SETTINGS_IX_CAPP_ID" ON "CRVN_LOG_SETTINGS" USING btree ("CAPP_ID");


--
-- TOC entry 1989 (class 1259 OID 19139)
-- Name: CRVN_SEC_APPS_UK_CRVN_SEC_APPS; Type: INDEX; Schema: caravan; Owner: postgres; Tablespace: 
--

CREATE UNIQUE INDEX "CRVN_SEC_APPS_UK_CRVN_SEC_APPS" ON "CRVN_SEC_APPS" USING btree ("CAPP_NAME");


--
-- TOC entry 1992 (class 1259 OID 19140)
-- Name: CRVN_SEC_CONTEXTS_UK_CRVN_SEC_CONTEXTS; Type: INDEX; Schema: caravan; Owner: postgres; Tablespace: 
--

CREATE UNIQUE INDEX "CRVN_SEC_CONTEXTS_UK_CRVN_SEC_CONTEXTS" ON "CRVN_SEC_CONTEXTS" USING btree ("CAPP_ID", "CCTX_NAME");


--
-- TOC entry 1998 (class 1259 OID 19144)
-- Name: CRVN_SEC_ENTRIES_IX_CGRP_ID; Type: INDEX; Schema: caravan; Owner: postgres; Tablespace: 
--

CREATE INDEX "CRVN_SEC_ENTRIES_IX_CGRP_ID" ON "CRVN_SEC_ENTRIES" USING btree ("CGRP_ID");


--
-- TOC entry 1999 (class 1259 OID 19142)
-- Name: CRVN_SEC_ENTRIES_IX_COBJ_ID; Type: INDEX; Schema: caravan; Owner: postgres; Tablespace: 
--

CREATE INDEX "CRVN_SEC_ENTRIES_IX_COBJ_ID" ON "CRVN_SEC_ENTRIES" USING btree ("COBJ_ID");


--
-- TOC entry 2000 (class 1259 OID 19145)
-- Name: CRVN_SEC_ENTRIES_IX_CROL_ID; Type: INDEX; Schema: caravan; Owner: postgres; Tablespace: 
--

CREATE INDEX "CRVN_SEC_ENTRIES_IX_CROL_ID" ON "CRVN_SEC_ENTRIES" USING btree ("CROL_ID");


--
-- TOC entry 2001 (class 1259 OID 19143)
-- Name: CRVN_SEC_ENTRIES_IX_CUSR_ID; Type: INDEX; Schema: caravan; Owner: postgres; Tablespace: 
--

CREATE INDEX "CRVN_SEC_ENTRIES_IX_CUSR_ID" ON "CRVN_SEC_ENTRIES" USING btree ("CUSR_ID");


--
-- TOC entry 2004 (class 1259 OID 19146)
-- Name: CRVN_SEC_GROUPS_UK_CRVN_SEC_GROUPS; Type: INDEX; Schema: caravan; Owner: postgres; Tablespace: 
--

CREATE UNIQUE INDEX "CRVN_SEC_GROUPS_UK_CRVN_SEC_GROUPS" ON "CRVN_SEC_GROUPS" USING btree ("CAPP_ID", "CGRP_NAME");


--
-- TOC entry 1995 (class 1259 OID 19141)
-- Name: CRVN_SEC_OBJECTS_UK_CRVN_SEC_OBJECTS; Type: INDEX; Schema: caravan; Owner: postgres; Tablespace: 
--

CREATE UNIQUE INDEX "CRVN_SEC_OBJECTS_UK_CRVN_SEC_OBJECTS" ON "CRVN_SEC_OBJECTS" USING btree ("CCTX_ID", "COBJ_NAME");


--
-- TOC entry 2007 (class 1259 OID 19147)
-- Name: CRVN_SEC_ROLES_UK_CRVN_SEC_ROLES; Type: INDEX; Schema: caravan; Owner: postgres; Tablespace: 
--

CREATE UNIQUE INDEX "CRVN_SEC_ROLES_UK_CRVN_SEC_ROLES" ON "CRVN_SEC_ROLES" USING btree ("CGRP_ID", "CROL_NAME");


--
-- TOC entry 2010 (class 1259 OID 19148)
-- Name: CRVN_SEC_USERS_UK_CRVN_SEC_USERS; Type: INDEX; Schema: caravan; Owner: postgres; Tablespace: 
--

CREATE UNIQUE INDEX "CRVN_SEC_USERS_UK_CRVN_SEC_USERS" ON "CRVN_SEC_USERS" USING btree ("CAPP_ID", "CUSR_LOGIN");


--
-- TOC entry 2020 (class 1259 OID 19152)
-- Name: CRVN_SEC_USER_GROUPS_IX_CGRP_ID; Type: INDEX; Schema: caravan; Owner: postgres; Tablespace: 
--

CREATE INDEX "CRVN_SEC_USER_GROUPS_IX_CGRP_ID" ON "CRVN_SEC_USER_GROUPS" USING btree ("CGRP_ID");


--
-- TOC entry 2021 (class 1259 OID 19153)
-- Name: CRVN_SEC_USER_GROUPS_IX_CUSR_ID; Type: INDEX; Schema: caravan; Owner: postgres; Tablespace: 
--

CREATE INDEX "CRVN_SEC_USER_GROUPS_IX_CUSR_ID" ON "CRVN_SEC_USER_GROUPS" USING btree ("CUSR_ID");


--
-- TOC entry 2016 (class 1259 OID 19150)
-- Name: CRVN_SEC_USER_ROLES_IX_CROL_ID; Type: INDEX; Schema: caravan; Owner: postgres; Tablespace: 
--

CREATE INDEX "CRVN_SEC_USER_ROLES_IX_CROL_ID" ON "CRVN_SEC_USER_ROLES" USING btree ("CROL_ID");


--
-- TOC entry 2017 (class 1259 OID 19151)
-- Name: CRVN_SEC_USER_ROLES_IX_CUSR_ID; Type: INDEX; Schema: caravan; Owner: postgres; Tablespace: 
--

CREATE INDEX "CRVN_SEC_USER_ROLES_IX_CUSR_ID" ON "CRVN_SEC_USER_ROLES" USING btree ("CUSR_ID");


--
-- TOC entry 2027 (class 2606 OID 19159)
-- Name: FK_caravan.CRVN_LOG_ENTRIES_caravan.CRVN_LOG_SETTINGS_CAPP_ID_C; Type: FK CONSTRAINT; Schema: caravan; Owner: postgres
--

ALTER TABLE ONLY "CRVN_LOG_ENTRIES"
    ADD CONSTRAINT "FK_caravan.CRVN_LOG_ENTRIES_caravan.CRVN_LOG_SETTINGS_CAPP_ID_C" FOREIGN KEY ("CAPP_ID", "CLOS_TYPE") REFERENCES "CRVN_LOG_SETTINGS"("CAPP_ID", "CLOS_TYPE") ON DELETE CASCADE;


--
-- TOC entry 2026 (class 2606 OID 19154)
-- Name: FK_caravan.CRVN_LOG_ENTRIES_caravan.CRVN_SEC_APPS_CAPP_ID; Type: FK CONSTRAINT; Schema: caravan; Owner: postgres
--

ALTER TABLE ONLY "CRVN_LOG_ENTRIES"
    ADD CONSTRAINT "FK_caravan.CRVN_LOG_ENTRIES_caravan.CRVN_SEC_APPS_CAPP_ID" FOREIGN KEY ("CAPP_ID") REFERENCES "CRVN_SEC_APPS"("CAPP_ID") ON DELETE CASCADE;


--
-- TOC entry 2037 (class 2606 OID 19209)
-- Name: FK_caravan.CRVN_LOG_SETTINGS_caravan.CRVN_SEC_APPS_CAPP_ID; Type: FK CONSTRAINT; Schema: caravan; Owner: postgres
--

ALTER TABLE ONLY "CRVN_LOG_SETTINGS"
    ADD CONSTRAINT "FK_caravan.CRVN_LOG_SETTINGS_caravan.CRVN_SEC_APPS_CAPP_ID" FOREIGN KEY ("CAPP_ID") REFERENCES "CRVN_SEC_APPS"("CAPP_ID") ON DELETE CASCADE;


--
-- TOC entry 2028 (class 2606 OID 19164)
-- Name: FK_caravan.CRVN_SEC_CONTEXTS_caravan.CRVN_SEC_APPS_CAPP_ID; Type: FK CONSTRAINT; Schema: caravan; Owner: postgres
--

ALTER TABLE ONLY "CRVN_SEC_CONTEXTS"
    ADD CONSTRAINT "FK_caravan.CRVN_SEC_CONTEXTS_caravan.CRVN_SEC_APPS_CAPP_ID" FOREIGN KEY ("CAPP_ID") REFERENCES "CRVN_SEC_APPS"("CAPP_ID") ON DELETE CASCADE;


--
-- TOC entry 2030 (class 2606 OID 19174)
-- Name: FK_caravan.CRVN_SEC_ENTRIES_caravan.CRVN_SEC_GROUPS_CGRP_ID; Type: FK CONSTRAINT; Schema: caravan; Owner: postgres
--

ALTER TABLE ONLY "CRVN_SEC_ENTRIES"
    ADD CONSTRAINT "FK_caravan.CRVN_SEC_ENTRIES_caravan.CRVN_SEC_GROUPS_CGRP_ID" FOREIGN KEY ("CGRP_ID") REFERENCES "CRVN_SEC_GROUPS"("CGRP_ID") ON DELETE CASCADE;


--
-- TOC entry 2031 (class 2606 OID 19179)
-- Name: FK_caravan.CRVN_SEC_ENTRIES_caravan.CRVN_SEC_OBJECTS_COBJ_ID; Type: FK CONSTRAINT; Schema: caravan; Owner: postgres
--

ALTER TABLE ONLY "CRVN_SEC_ENTRIES"
    ADD CONSTRAINT "FK_caravan.CRVN_SEC_ENTRIES_caravan.CRVN_SEC_OBJECTS_COBJ_ID" FOREIGN KEY ("COBJ_ID") REFERENCES "CRVN_SEC_OBJECTS"("COBJ_ID") ON DELETE CASCADE;


--
-- TOC entry 2032 (class 2606 OID 19184)
-- Name: FK_caravan.CRVN_SEC_ENTRIES_caravan.CRVN_SEC_ROLES_CROL_ID; Type: FK CONSTRAINT; Schema: caravan; Owner: postgres
--

ALTER TABLE ONLY "CRVN_SEC_ENTRIES"
    ADD CONSTRAINT "FK_caravan.CRVN_SEC_ENTRIES_caravan.CRVN_SEC_ROLES_CROL_ID" FOREIGN KEY ("CROL_ID") REFERENCES "CRVN_SEC_ROLES"("CROL_ID") ON DELETE CASCADE;


--
-- TOC entry 2033 (class 2606 OID 19189)
-- Name: FK_caravan.CRVN_SEC_ENTRIES_caravan.CRVN_SEC_USERS_CUSR_ID; Type: FK CONSTRAINT; Schema: caravan; Owner: postgres
--

ALTER TABLE ONLY "CRVN_SEC_ENTRIES"
    ADD CONSTRAINT "FK_caravan.CRVN_SEC_ENTRIES_caravan.CRVN_SEC_USERS_CUSR_ID" FOREIGN KEY ("CUSR_ID") REFERENCES "CRVN_SEC_USERS"("CUSR_ID") ON DELETE CASCADE;


--
-- TOC entry 2034 (class 2606 OID 19194)
-- Name: FK_caravan.CRVN_SEC_GROUPS_caravan.CRVN_SEC_APPS_CAPP_ID; Type: FK CONSTRAINT; Schema: caravan; Owner: postgres
--

ALTER TABLE ONLY "CRVN_SEC_GROUPS"
    ADD CONSTRAINT "FK_caravan.CRVN_SEC_GROUPS_caravan.CRVN_SEC_APPS_CAPP_ID" FOREIGN KEY ("CAPP_ID") REFERENCES "CRVN_SEC_APPS"("CAPP_ID") ON DELETE CASCADE;


--
-- TOC entry 2029 (class 2606 OID 19169)
-- Name: FK_caravan.CRVN_SEC_OBJECTS_caravan.CRVN_SEC_CONTEXTS_CCTX_ID; Type: FK CONSTRAINT; Schema: caravan; Owner: postgres
--

ALTER TABLE ONLY "CRVN_SEC_OBJECTS"
    ADD CONSTRAINT "FK_caravan.CRVN_SEC_OBJECTS_caravan.CRVN_SEC_CONTEXTS_CCTX_ID" FOREIGN KEY ("CCTX_ID") REFERENCES "CRVN_SEC_CONTEXTS"("CCTX_ID") ON DELETE CASCADE;


--
-- TOC entry 2035 (class 2606 OID 19199)
-- Name: FK_caravan.CRVN_SEC_ROLES_caravan.CRVN_SEC_GROUPS_CGRP_ID; Type: FK CONSTRAINT; Schema: caravan; Owner: postgres
--

ALTER TABLE ONLY "CRVN_SEC_ROLES"
    ADD CONSTRAINT "FK_caravan.CRVN_SEC_ROLES_caravan.CRVN_SEC_GROUPS_CGRP_ID" FOREIGN KEY ("CGRP_ID") REFERENCES "CRVN_SEC_GROUPS"("CGRP_ID") ON DELETE CASCADE;


--
-- TOC entry 2036 (class 2606 OID 19204)
-- Name: FK_caravan.CRVN_SEC_USERS_caravan.CRVN_SEC_APPS_CAPP_ID; Type: FK CONSTRAINT; Schema: caravan; Owner: postgres
--

ALTER TABLE ONLY "CRVN_SEC_USERS"
    ADD CONSTRAINT "FK_caravan.CRVN_SEC_USERS_caravan.CRVN_SEC_APPS_CAPP_ID" FOREIGN KEY ("CAPP_ID") REFERENCES "CRVN_SEC_APPS"("CAPP_ID") ON DELETE CASCADE;


--
-- TOC entry 2040 (class 2606 OID 19224)
-- Name: FK_caravan.CRVN_SEC_USER_GROUPS_caravan.CRVN_SEC_GROUPS_CGRP_ID; Type: FK CONSTRAINT; Schema: caravan; Owner: postgres
--

ALTER TABLE ONLY "CRVN_SEC_USER_GROUPS"
    ADD CONSTRAINT "FK_caravan.CRVN_SEC_USER_GROUPS_caravan.CRVN_SEC_GROUPS_CGRP_ID" FOREIGN KEY ("CGRP_ID") REFERENCES "CRVN_SEC_GROUPS"("CGRP_ID") ON DELETE CASCADE;


--
-- TOC entry 2041 (class 2606 OID 19229)
-- Name: FK_caravan.CRVN_SEC_USER_GROUPS_caravan.CRVN_SEC_USERS_CUSR_ID; Type: FK CONSTRAINT; Schema: caravan; Owner: postgres
--

ALTER TABLE ONLY "CRVN_SEC_USER_GROUPS"
    ADD CONSTRAINT "FK_caravan.CRVN_SEC_USER_GROUPS_caravan.CRVN_SEC_USERS_CUSR_ID" FOREIGN KEY ("CUSR_ID") REFERENCES "CRVN_SEC_USERS"("CUSR_ID") ON DELETE CASCADE;


--
-- TOC entry 2038 (class 2606 OID 19214)
-- Name: FK_caravan.CRVN_SEC_USER_ROLES_caravan.CRVN_SEC_ROLES_CROL_ID; Type: FK CONSTRAINT; Schema: caravan; Owner: postgres
--

ALTER TABLE ONLY "CRVN_SEC_USER_ROLES"
    ADD CONSTRAINT "FK_caravan.CRVN_SEC_USER_ROLES_caravan.CRVN_SEC_ROLES_CROL_ID" FOREIGN KEY ("CROL_ID") REFERENCES "CRVN_SEC_ROLES"("CROL_ID") ON DELETE CASCADE;


--
-- TOC entry 2039 (class 2606 OID 19219)
-- Name: FK_caravan.CRVN_SEC_USER_ROLES_caravan.CRVN_SEC_USERS_CUSR_ID; Type: FK CONSTRAINT; Schema: caravan; Owner: postgres
--

ALTER TABLE ONLY "CRVN_SEC_USER_ROLES"
    ADD CONSTRAINT "FK_caravan.CRVN_SEC_USER_ROLES_caravan.CRVN_SEC_USERS_CUSR_ID" FOREIGN KEY ("CUSR_ID") REFERENCES "CRVN_SEC_USERS"("CUSR_ID") ON DELETE CASCADE;


--
-- TOC entry 2157 (class 0 OID 0)
-- Dependencies: 5
-- Name: public; Type: ACL; Schema: -; Owner: postgres
--

REVOKE ALL ON SCHEMA public FROM PUBLIC;
REVOKE ALL ON SCHEMA public FROM postgres;
GRANT ALL ON SCHEMA public TO postgres;
GRANT ALL ON SCHEMA public TO PUBLIC;


-- Completed on 2015-03-01 23:44:50

--
-- PostgreSQL database dump complete
--

