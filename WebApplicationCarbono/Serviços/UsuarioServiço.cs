﻿using Npgsql;
using WebApplicationCarbono.Dtos;
using WebApplicationCarbono.Interface;
using WebApplicationCarbono.Modelos;

namespace WebApplicationCarbono.Serviços
{
    public class UsuarioServiço : IUsuario
    {
        // passando a conexão do banco
        private readonly String _stringConexao;
        public UsuarioServiço(IConfiguration configuaração)
        {
            _stringConexao = configuaração.GetConnectionString("DefaultConnection");
        }

        public void CadastrarUsuario(CadastroUsuarioDto cadastroUsuarioDto)
        {
            try
            {
                using (var conexao = new NpgsqlConnection(_stringConexao))
                {
                    conexao.Open();

                    string query = @"
                        INSERT INTO usuarios (nome, email, senha, empresa, cnpj, telefone)
                        VALUES (@nome, @email, @senha, @empresa, @cnpj, @telefone);
                    ";

                    using (var comando = new NpgsqlCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("@nome", cadastroUsuarioDto.Nome);
                        comando.Parameters.AddWithValue("@email", cadastroUsuarioDto.Email);
                        comando.Parameters.AddWithValue("@senha", cadastroUsuarioDto.Senha); // Ideal: hash da senha
                        comando.Parameters.AddWithValue("@empresa", cadastroUsuarioDto.Empresa);
                        comando.Parameters.AddWithValue("@cnpj", cadastroUsuarioDto.CNPJ);
                        comando.Parameters.AddWithValue("@telefone", cadastroUsuarioDto.Telefone);

                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao cadastrar usuário: " + ex.Message);
            }
        }

        public BuscarUsuario GetUsuario(int IdUsuario)
        {
            BuscarUsuario usuario = null;

            try
            {
                using (var conexao = new NpgsqlConnection(_stringConexao))
                {
                    conexao.Open();

                    var query = "SELECT * FROM usuarios WHERE id = @Id";
                    using (var comando = new NpgsqlCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("Id", IdUsuario);

                        using (var reader = comando.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                usuario = new BuscarUsuario
                                {
                                    codigoCadastro = reader.GetInt32(reader.GetOrdinal("id")),
                                    Nome = reader.GetString(reader.GetOrdinal("nome")),
                                    Email = reader.GetString(reader.GetOrdinal("email")),
                                    empresa = reader.GetString(reader.GetOrdinal("empresa")),
                                    CNPJ = reader.GetString(reader.GetOrdinal("cnpj")),
                                    Telefone = reader.GetString(reader.GetOrdinal("telefone"))
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao consultar usuário: " + ex.Message);
            }

            return usuario;
        }
    }
}
