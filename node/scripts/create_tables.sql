-- Creation of pessoas table
CREATE TABLE IF NOT EXISTS pessoas (
  id uuid NOT NULL,
  apelido varchar(32) NOT NULL,
  nome varchar(100) NOT NULL,
  nascimento Date NOT NULL,
  stack varchar(1000),
  PRIMARY KEY (id)
);

CREATE EXTENSION pg_trgm;

CREATE INDEX idx_gist ON pessoas USING gist (nome gist_trgm_ops, apelido gist_trgm_ops, stack gist_trgm_ops );
