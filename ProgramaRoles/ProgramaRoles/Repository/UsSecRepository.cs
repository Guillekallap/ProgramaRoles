﻿using ProgramaRoles.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProgramaRoles.Repository
{
    public class UsSecRepository
    {
        private SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["getconn"].ToString();
            con = new SqlConnection(constr);
        }

        public List<UsuariosSectores> ListarTodosUsuariosSectores(string dni, string nombreUsuario, string nombreSector)
        {
            connection();
            List<UsuariosSectores> listaUsSec = new List<UsuariosSectores>();  
            SqlCommand com = new SqlCommand("ListarUsuarioSector", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            try
            {
                
                con.Open();
                com.Parameters.AddWithValue("@dni", string.IsNullOrEmpty(dni)? (object)DBNull.Value: dni);
                com.Parameters.AddWithValue("@nombreUsuario", string.IsNullOrEmpty(nombreUsuario) ? (object)DBNull.Value : nombreUsuario);
                com.Parameters.AddWithValue("@nombreSector", string.IsNullOrEmpty(nombreSector) ? (object)DBNull.Value : nombreSector);

                da.Fill(dt);
                con.Close();
                listaUsSec = (from DataRow dr in dt.Rows

                              select new UsuariosSectores()
                              {
                                  id = Convert.ToInt32(dr["id"]),
                                  idSector = Convert.ToInt32(dr["idSector"]),
                                  nombreSector = Convert.ToString(dr["nombreSector"]),
                                  idUsuario = Convert.ToInt32(dr["idUsuario"]),
                                  nombreUsuario = Convert.ToString(dr["nombreUsuario"]),
                                  dni = Convert.ToString(dr["dni"]),
                                  email = Convert.ToString(dr["email"]),
                                  roles = Convert.ToString(dr["roles"]),
                              }).ToList();

                return listaUsSec;


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public UsuariosSectores BuscarUsuarioSector(int i)
        {

            connection();
            SqlCommand com = new SqlCommand("BuscarUsuarioSector", con);

            UsuariosSectores usec = new UsuariosSectores();
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            com.Parameters.AddWithValue("@id", i);

            try {

                con.Open();
                da.Fill(dt);
                con.Close();
                usec = (from DataRow dr in dt.Rows
                        select new UsuariosSectores()
                        {

                            id = Convert.ToInt32(dr["id"]),
                            idSector = Convert.ToInt32(dr["idSector"]),
                            nombreSector = Convert.ToString(dr["nombreSector"]),
                            idUsuario = Convert.ToInt32(dr["idUsuario"]),
                            nombreUsuario = Convert.ToString(dr["nombreUsuario"]),
                            dni = Convert.ToString(dr["dni"]),
                            email = Convert.ToString(dr["email"]),
                            roles = Convert.ToString(dr["roles"]),
                        }).FirstOrDefault();




            return (usec);


            }catch(Exception e){
                throw new Exception(e.Message);

            }

        }

        public List<Roles> ListarTodosRoles()
        {
            connection();
            List<Roles> listarol = new List<Roles>();

            SqlCommand com = new SqlCommand("ListarRoles", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();

            listarol = (from DataRow dr in dt.Rows

                          select new Roles()
                          {
                              id = Convert.ToInt32(dr["id"]),
                              rol = Convert.ToString(dr["rol"]),
                              descripcion = Convert.ToString(dr["descripcion"]),
                          }).ToList();

            return listarol;
        }

        public Roles BuscarRol(string rol)
        {

            connection();
            SqlCommand com = new SqlCommand("BuscarRol", con);

            Roles roles = new Roles();
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            com.Parameters.AddWithValue("@rol", rol);

            try
            {

                con.Open();
                da.Fill(dt);
                con.Close();
                roles = (from DataRow dr in dt.Rows
                        select new Roles()
                        {

                            id = Convert.ToInt32(dr["id"]),
                            rol = Convert.ToString(dr["rol"]),
                            descripcion = Convert.ToString(dr["descripcion"]),

                        }).FirstOrDefault();

                return (roles);


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }
        }

        public void ModificarRolesUsuarioSector(int id, string roles)
        {
                connection();
                SqlCommand com = new SqlCommand("ModificarRolesUsuarioSector", con);

                com.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                
                com.Parameters.AddWithValue("@id", id);

                com.Parameters.AddWithValue("@roles", string.IsNullOrEmpty(roles) ? (object)DBNull.Value : roles);

            try
            {
                con.Open();
                da.Fill(dt);
                con.Close();

            }catch(Exception e){
                throw new Exception(e.Message);
            }
        }

        public void AgregarUsuarioSectorRolHorario(int idUsuarioSector, string nombreUsuario, string rolesAnteriores, string rolesNuevos, string email, bool emailChked, DateTime fechaInicio, DateTime fechaFin, DateTime fechaActual, bool vigente)
        {
            
            string constr = ConfigurationManager.ConnectionStrings["klinicos_interno"].ToString();
            con = new SqlConnection(constr);
            SqlCommand com = new SqlCommand("AgregarUsuarioSectorRolHorario", con);

            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            
            com.Parameters.AddWithValue("@idUsuarioSector", idUsuarioSector);
            com.Parameters.AddWithValue("@nombreUsuario", string.IsNullOrEmpty(nombreUsuario) ? (object)DBNull.Value : nombreUsuario);
            com.Parameters.AddWithValue("@rolesAnteriores", string.IsNullOrEmpty(rolesAnteriores) ? (object)DBNull.Value : rolesAnteriores);
            com.Parameters.AddWithValue("@rolesNuevos", string.IsNullOrEmpty(rolesNuevos) ? (object)DBNull.Value : rolesNuevos);
            com.Parameters.AddWithValue("@email", string.IsNullOrEmpty(email) ? (object)DBNull.Value : email);
            com.Parameters.AddWithValue("@emailChked", emailChked ? (object)DBNull.Value : emailChked);
            com.Parameters.AddWithValue("@fechaInicio", fechaInicio);
            com.Parameters.AddWithValue("@fechaFin", fechaFin);
            com.Parameters.AddWithValue("@fechaActual", fechaActual);
            com.Parameters.AddWithValue("@vigente", vigente);
            
            try
            {
                con.Open();
                da.Fill(dt);
                con.Close();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<UsuarioRolHorario> ListarUsuarioRolHorario(int idUsuarioSector, DateTime fechaInicio, DateTime fechaFin)
        {
            string constr = ConfigurationManager.ConnectionStrings["klinicos_interno"].ToString();
            con = new SqlConnection(constr);
            List<UsuarioRolHorario> listaUsRolHorario = new List<UsuarioRolHorario>();
            SqlCommand com = new SqlCommand("ListarUsuarioRolHorario", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            try
            {

                con.Open();
                com.Parameters.AddWithValue("@idUsuarioSector", idUsuarioSector);
                com.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                com.Parameters.AddWithValue("@fechaFin", fechaFin);

                da.Fill(dt);
                con.Close();
                listaUsRolHorario = (from DataRow dr in dt.Rows

                                     select new UsuarioRolHorario()
                                     {
                                         id = Convert.ToInt32(dr["id"]),
                                         idUsuarioSector = Convert.ToInt32(dr["idUsuarioSector"]),
                                         nombreUsuario = Convert.ToString(dr["nombreUsuario"]),
                                         rolesAnteriores = Convert.ToString(dr["rolesAnteriores"]),
                                         rolesNuevos = Convert.ToString(dr["rolesNuevos"]),
                                         email = Convert.ToString(dr["email"]),
                                         emailChked = Convert.ToBoolean(dr["emailChked"]),
                                         fechaInicio = Convert.ToDateTime(dr["fechaInicio"]),
                                         fechaFin = Convert.ToDateTime(dr["fechaFin"]),
                                         fechaActual = Convert.ToDateTime(dr["fechaActual"]),
                                         vigente = Convert.ToBoolean(dr["vigente"])

                                     }).ToList();

                return listaUsRolHorario;


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        
        
        //public UsuariosSectores BuscarUsuarioSectorPorRol(int id, int idSector, int idUsuario, string roles)
        //{

        //    connection();
        //    SqlCommand com = new SqlCommand("BuscarUsuarioSectorPorRol", con);

        //    UsuariosSectores Usec = new UsuariosSectores();
        //    com.CommandType = CommandType.StoredProcedure;
        //    SqlDataAdapter da = new SqlDataAdapter(com);
        //    DataTable dt = new DataTable();

        //    com.Parameters.AddWithValue("@id", id);
        //    com.Parameters.AddWithValue("@idSector", idSector);
        //    com.Parameters.AddWithValue("@idUsuario", idUsuario);
        //    com.Parameters.AddWithValue("@roles", roles);
        //    try
        //    {

        //        con.Open();
        //        da.Fill(dt);
        //        con.Close();
        //        Usec = (from DataRow dr in dt.Rows
        //                     select new UsuariosSectores()
        //                     {

        //                         id = Convert.ToInt32(dr["id"]),
        //                         idSector = Convert.ToInt32(dr["idSector"]),
        //                         nombreSector = Convert.ToString(dr["nombreSector"]),
        //                         idUsuario = Convert.ToInt32(dr["idUsuario"]),
        //                         nombreUsuario = Convert.ToString(dr["nombreUsuario"]),
        //                         dni = Convert.ToString(dr["dni"]),
        //                         roles = Convert.ToString(dr["roles"]),
        //                     });




        //        return (Usec);


        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);

        //    }


    }
}