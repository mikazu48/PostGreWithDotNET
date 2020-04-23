--
-- PostgreSQL database dump
--

-- Dumped from database version 12.2
-- Dumped by pg_dump version 12.2

-- Started on 2020-04-23 00:28:25

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 229 (class 1255 OID 32768)
-- Name: tblaccount_checkaccount(character varying, character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.tblaccount_checkaccount(_username character varying, _password character varying) RETURNS TABLE(countdata bigint)
    LANGUAGE plpgsql
    AS $$
begin
return query
select count(*) from tblaccount where Username = _Username and Password = _Password;
end
$$;


ALTER FUNCTION public.tblaccount_checkaccount(_username character varying, _password character varying) OWNER TO postgres;

--
-- TOC entry 212 (class 1255 OID 32769)
-- Name: tblaccount_delete(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.tblaccount_delete(_account_id character varying) RETURNS integer
    LANGUAGE plpgsql
    AS $$
begin 
delete from tblaccount
where account_ID = _account_ID;
if found then 
	return 1;
else return 0;
end if;
end
$$;


ALTER FUNCTION public.tblaccount_delete(_account_id character varying) OWNER TO postgres;

--
-- TOC entry 205 (class 1255 OID 32780)
-- Name: tblaccount_insert(character varying, character varying, character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.tblaccount_insert(_username character varying, _password character varying, _employee_id character varying) RETURNS integer
    LANGUAGE plpgsql
    AS $$
begin 
insert into tblaccount
values(_username,_password,_employee_id);
if found then 
	return 1;
else return 0;
end if;
end
$$;


ALTER FUNCTION public.tblaccount_insert(_username character varying, _password character varying, _employee_id character varying) OWNER TO postgres;

--
-- TOC entry 206 (class 1255 OID 32782)
-- Name: tblaccount_select(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.tblaccount_select() RETURNS TABLE(username character varying, password character varying, employee_id character varying)
    LANGUAGE plpgsql
    AS $$
begin 
return query
select * from tblaccount;

end
$$;


ALTER FUNCTION public.tblaccount_select() OWNER TO postgres;

--
-- TOC entry 230 (class 1255 OID 32781)
-- Name: tblaccount_update(character varying, character varying, character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.tblaccount_update(_username character varying, _password character varying, _employee_id character varying) RETURNS integer
    LANGUAGE plpgsql
    AS $$
begin 
update tblaccount
set
password = _password
where username = _username;
if found then 
	return 1;
else return 0;
end if;
end
$$;


ALTER FUNCTION public.tblaccount_update(_username character varying, _password character varying, _employee_id character varying) OWNER TO postgres;

--
-- TOC entry 210 (class 1255 OID 24637)
-- Name: tblarea_delete(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.tblarea_delete(_area_id character varying) RETURNS integer
    LANGUAGE plpgsql
    AS $$
begin 
delete from tblArea

where area_id = _area_id;
if found then 
	return 1;
else return 0;
end if;
end
$$;


ALTER FUNCTION public.tblarea_delete(_area_id character varying) OWNER TO postgres;

--
-- TOC entry 208 (class 1255 OID 24634)
-- Name: tblarea_insert(character varying, character varying, character varying, character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.tblarea_insert(_area_id character varying, _area_name character varying, _regional character varying, _district character varying) RETURNS integer
    LANGUAGE plpgsql
    AS $$
begin 
insert into tblArea
values(_area_id,_area_name,_regional,_district);
if found then 
	return 1;
else return 0;
end if;
end
$$;


ALTER FUNCTION public.tblarea_insert(_area_id character varying, _area_name character varying, _regional character varying, _district character varying) OWNER TO postgres;

--
-- TOC entry 209 (class 1255 OID 24636)
-- Name: tblarea_select(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.tblarea_select() RETURNS TABLE(area_id character varying, area_name character varying, regional character varying, district character varying)
    LANGUAGE plpgsql
    AS $$
begin 
return query
select * from tblArea;

end
$$;


ALTER FUNCTION public.tblarea_select() OWNER TO postgres;

--
-- TOC entry 207 (class 1255 OID 24635)
-- Name: tblarea_update(character varying, character varying, character varying, character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.tblarea_update(_area_id character varying, _area_name character varying, _regional character varying, _district character varying) RETURNS integer
    LANGUAGE plpgsql
    AS $$
begin 
update tblArea
set
area_name = _area_name,
regional =_regional,
district =_district
where area_id = _area_id;
if found then 
	return 1;
else return 0;
end if;
end
$$;


ALTER FUNCTION public.tblarea_update(_area_id character varying, _area_name character varying, _regional character varying, _district character varying) OWNER TO postgres;

--
-- TOC entry 225 (class 1255 OID 24587)
-- Name: tblemployee_delete(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.tblemployee_delete(_employee_id character varying) RETURNS integer
    LANGUAGE plpgsql
    AS $$
begin 
delete from tblemployee
where employee_ID = _employee_ID;
if found then 
	return 1;
else return 0;
end if;
end
$$;


ALTER FUNCTION public.tblemployee_delete(_employee_id character varying) OWNER TO postgres;

--
-- TOC entry 227 (class 1255 OID 24624)
-- Name: tblemployee_insert(character varying, character varying, character, date, character varying, character varying, text, character varying, character varying, character varying, character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.tblemployee_insert(_employee_id character varying, _employee_name character varying, _gender character, _birth_date date, _email character varying, _contact_phone character varying, _address text, _position_id character varying, _division_id character varying, _supervisor_id character varying, _image_path character varying) RETURNS integer
    LANGUAGE plpgsql
    AS $$
begin 
insert into tblemployee
values(_employee_ID,_employee_name,_gender,_birth_date,_email,_contact_phone,_address,_position_id,_division_id,_supervisor_id,_image_path);
if found then 
	return 1;
else return 0;
end if;
end
$$;


ALTER FUNCTION public.tblemployee_insert(_employee_id character varying, _employee_name character varying, _gender character, _birth_date date, _email character varying, _contact_phone character varying, _address text, _position_id character varying, _division_id character varying, _supervisor_id character varying, _image_path character varying) OWNER TO postgres;

--
-- TOC entry 228 (class 1255 OID 24625)
-- Name: tblemployee_select(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.tblemployee_select() RETURNS TABLE(employee_id character varying, employee_name character varying, gender character, birth_date date, email character varying, contact_phone character varying, address text, position_id character varying, division_id character varying, supervisor_id character varying, image_path character varying)
    LANGUAGE plpgsql
    AS $$
begin 
return query
select * from tblemployee;

end
$$;


ALTER FUNCTION public.tblemployee_select() OWNER TO postgres;

--
-- TOC entry 226 (class 1255 OID 24622)
-- Name: tblemployee_update(character varying, character varying, character, date, character varying, character varying, text, character varying, character varying, character varying, character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.tblemployee_update(_employee_id character varying, _employee_name character varying, _gender character, _birth_date date, _email character varying, _contact_phone character varying, _address text, _position_id character varying, _division_id character varying, _supervisor_id character varying, _image_path character varying) RETURNS integer
    LANGUAGE plpgsql
    AS $$
begin 
update tblemployee
set
employee_name = _employee_name,
gender =_gender,
birth_date =_birth_date,
email = _email,
contact_phone = _contact_phone,
address =_address,
position_id =_position_id,
division_id =_division_id,
supervisor_id =_supervisor_id,
image_path =_image_path
where employee_ID = _employee_ID;
if found then 
	return 1;
else return 0;
end if;
end
$$;


ALTER FUNCTION public.tblemployee_update(_employee_id character varying, _employee_name character varying, _gender character, _birth_date date, _email character varying, _contact_phone character varying, _address text, _position_id character varying, _division_id character varying, _supervisor_id character varying, _image_path character varying) OWNER TO postgres;

--
-- TOC entry 211 (class 1255 OID 16497)
-- Name: tbluser_checkuser(character varying, character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.tbluser_checkuser(_username character varying, _password character varying) RETURNS TABLE(countdata bigint)
    LANGUAGE plpgsql
    AS $$
begin
return query
select count(*) from tblUser where Username = _Username and Password = _Password;
end
$$;


ALTER FUNCTION public.tbluser_checkuser(_username character varying, _password character varying) OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 204 (class 1259 OID 32776)
-- Name: tblaccount; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.tblaccount (
    username character varying(20) NOT NULL,
    password character varying(255) NOT NULL,
    employee_id character varying(20) NOT NULL
);


ALTER TABLE public.tblaccount OWNER TO postgres;

--
-- TOC entry 203 (class 1259 OID 24626)
-- Name: tblarea; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.tblarea (
    area_id character varying(20) NOT NULL,
    area_name character varying(200),
    regional character varying(200),
    district character varying(200)
);


ALTER TABLE public.tblarea OWNER TO postgres;

--
-- TOC entry 202 (class 1259 OID 24613)
-- Name: tblemployee; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.tblemployee (
    employee_id character varying(20) NOT NULL,
    employee_name character varying(250) NOT NULL,
    gender character(1) NOT NULL,
    birth_date date NOT NULL,
    email character varying(100) NOT NULL,
    contact_phone character varying(20) NOT NULL,
    address text NOT NULL,
    position_id character varying(50) NOT NULL,
    division_id character varying(100) NOT NULL,
    supervisor_id character varying(100) NOT NULL,
    image_path character varying(255) NOT NULL
);


ALTER TABLE public.tblemployee OWNER TO postgres;

--
-- TOC entry 2841 (class 0 OID 32776)
-- Dependencies: 204
-- Data for Name: tblaccount; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.tblaccount (username, password, employee_id) FROM stdin;
mikazu	a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3	EM0001
eramus	ca978112ca1bbdcafac231b39a23dc4da786eff8147c4e72b9807785afee48bb	EM0004
\.


--
-- TOC entry 2840 (class 0 OID 24626)
-- Dependencies: 203
-- Data for Name: tblarea; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.tblarea (area_id, area_name, regional, district) FROM stdin;
JKT002	Grogol	DKI Jakarta	Jakarta Barat
JKT003	Harmoni	DKI Jakarta	Jakarta Pusat
JWB001	Bogor Kota	Jawa Barat	Bogor
JWT001	Malang Kota	Jawa Timur	Malang
JKT001	Condet 0	DKI Jakarta	Jakarta Timur
\.


--
-- TOC entry 2839 (class 0 OID 24613)
-- Dependencies: 202
-- Data for Name: tblemployee; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.tblemployee (employee_id, employee_name, gender, birth_date, email, contact_phone, address, position_id, division_id, supervisor_id, image_path) FROM stdin;
EM0003	Sean Rosales	F	1995-06-26	erat@Crasei.ca	089175058753	P.O. Box 231, 1604 Tortor. Avenue	Staff	Leader	Pak B	IMG_4670-20200422-103057.PNG
EM0004	Erasmus Joyce	F	1993-05-04	posuere@Proinnon.org	088365172584	Ap #323-2454 Elit, St.	Staff	Analyst	Pak C	IMG_4615-20200422-103811.PNG
EM0006	Shani Indira	F	1999-10-10	shani@shani.com	0812345678	Senayan , Jakarta Selatan	Staff	Analyst	Pak C	Shani-20200422-110856.jpg
EM0001	Dimas Syahputra	M	1998-10-17	renchadon@gmail.com	081284216087	Inerbang, Condet - Jakarta Timur	Staff	Leader	Pak B	profile-20200422-084950.jpg
EM0002	Damian Vargas	M	1997-10-01	cursus@Nullaf.com	086252064945	Ap #605-7215 Vivamus Ave	Manager	Analyst	Pak A	CUOU5338-20200422-102903.JPG
\.


--
-- TOC entry 2712 (class 2606 OID 24633)
-- Name: tblarea tblarea_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tblarea
    ADD CONSTRAINT tblarea_pkey PRIMARY KEY (area_id);


--
-- TOC entry 2710 (class 2606 OID 24620)
-- Name: tblemployee tblemployee_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tblemployee
    ADD CONSTRAINT tblemployee_pkey PRIMARY KEY (employee_id);


-- Completed on 2020-04-23 00:28:26

--
-- PostgreSQL database dump complete
--

