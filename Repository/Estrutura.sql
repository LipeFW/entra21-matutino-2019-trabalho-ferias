CREATE TABLE categorias(
	id INT PRIMARY KEY IDENTITY(1,1),
	nome VARCHAR(100)
);

CREATE TABLE usuarios(
	id INT PRIMARY KEY IDENTITY(1,1),
	nome VARCHAR(100),
	login VARCHAR(100),
	senha VARCHAR(100)

);

CREATE TABLE estados(
	id INT PRIMARY KEY IDENTITY(1,1),
	nome VARCHAR(100),
	sigla VARCHAR(2)
);

CREATE TABLE cidades(
	id INT PRIMARY KEY IDENTITY(1,1),
	id_estado INT,
	FOREIGN KEY (id_estado) REFERENCES estados(id),
	nome VARCHAR(100),
	numero_habitantes INT
)

CREATE TABLE clientes(
	id INT PRIMARY KEY IDENTITY(1,1),
	id_cidade INT,
	FOREIGN KEY (id_cidade) REFERENCES cidades(id),
	nome VARCHAR(100),
	cpf VARCHAR(15),
	data_nascimento DATETIME2,
	numero INT,
	complemento VARCHAR(100),
	logradouro VARCHAR(100),
	cep VARCHAR(100)
);

CREATE TABLE projetos(
	id INT PRIMARY KEY IDENTITY(1,1),
	id_cliente INT,
	FOREIGN KEY (id_cliente) REFERENCES clientes(id),
	nome VARCHAR(100),
	data_criacao DATETIME2,
	data_finalizacao DATETIME2

);

CREATE TABLE tarefas(
	id INT PRIMARY KEY IDENTITY(1,1),
	id_usuario_responsavel INT,
	id_projeto INT,
	id_categoria INT,
	FOREIGN KEY(id_usuario_responsavel) REFERENCES usuarios (id),
	FOREIGN KEY(id_projeto) REFERENCES projetos(id),
	FOREIGN KEY(id_categoria) REFERENCES categorias(id),
	titulo VARCHAR(200),
	descricao TEXT ,
	duracao DATETIME2
);