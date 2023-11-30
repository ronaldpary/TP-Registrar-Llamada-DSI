﻿ using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2023.Entidades
{
    public class Llamada
    {
        #region Atributos
        public int id { get; set; }
        DateTime fechaHoraInicioLlamada;
        string descripcionOperador;
        string detalleAccionRequerida;
        double duracion;
        Accion accionRequerida;
        List<CambioEstado> cambioEstado;
        Cliente cliente;
        SubOpcionLlamada subOpcionSeleccionada;
        OpcionLlamada opcionSeleccionada;
        EstadoLlamada estadoActual;

        #endregion


        #region Getters/Setters
        public void agregarCambioEstado(CambioEstado nuevoCambioEstado) { this.cambioEstado.Add(nuevoCambioEstado); }
        public EstadoLlamada getEstadoActual() { return this.estadoActual; }
        public void setEstado(EstadoLlamada estado) { this.estadoActual = estado; }
        public string getDescripcionOperador() { return descripcionOperador; }
        public string getDetalleAccionRequerida() { return detalleAccionRequerida; }
        public double getDuracion() { return duracion; }
        public Accion getAccionRequerida() { return accionRequerida; }
        public List<CambioEstado> getCambiosEstados() { return cambioEstado; }
        public Cliente getCliente() { return cliente; }
        public SubOpcionLlamada getSubOpcion() { return subOpcionSeleccionada; }
        public OpcionLlamada getOpcion() { return opcionSeleccionada; }
        public void setDescripcionOperador(string descripcionOperador) { this.descripcionOperador = descripcionOperador; }
        public void setDuracion(double duracion) { this.duracion = duracion; }
        public void setOpcion(OpcionLlamada opcionSeleccionada) { this.opcionSeleccionada = opcionSeleccionada; }
        public void setSubOpcion(SubOpcionLlamada subOpcionSeleccionada) { this.subOpcionSeleccionada = subOpcionSeleccionada; }

        #endregion


        #region Constructor
        public Llamada(Cliente cliente, CambioEstado cambioEstado, int id)
        {
            this.id = id;
            this.cliente = cliente;
            this.cambioEstado = new List<CambioEstado> { cambioEstado };
        }
        #endregion


        #region Metodos

        //Metodo que crea un cambio de estado nuevo a la llamada y cierra el anterior
        public void cambiarEstado(EstadoLlamada estado, DateTime fechaHoraActual)
        {
            EstadoLlamada estact = estado;

            CambioEstado actualcambio = obtenerEstadoActual(cambioEstado);
            actualcambio.setFechaHoraFin(fechaHoraActual);

            // Actualizando el cambio estado en la bdd para agregarle la fecha fin
            CambiosEstado.Update(actualcambio);


            CambioEstado nuevoCambioEstado = new CambioEstado(fechaHoraActual, estado);

            this.setEstado(estado);

            // Inserta en nuevo cambioEstado en la BDD
            CambiosEstado.Insert(nuevoCambioEstado, this.id);

            nuevoCambioEstado.id =  CambiosEstado.getIdCambioactual();

            cambioEstado.Add(nuevoCambioEstado);
        }

        //Metodo que obtiene el ultimo CambioEstado 
        private CambioEstado obtenerEstadoActual(List<CambioEstado> cambiosEstado)
        {
            foreach (CambioEstado cambio in cambiosEstado)  {
                if (cambio.esUltimo())
                {
                    return cambio;
                }
            }

            return null;
        }

        //Método que obtiene el nombre del cliente de la llamada
        public string getNombreClienteLlamada() { return this.cliente.getNombreCompleto(); }

        //Método que calcula la duración de la llamada
        public double calcularDuracion(DateTime fechaHoraFin)
        {
            obtenerFechaHoraInicioLlamada();
            return (fechaHoraFin - fechaHoraInicioLlamada).TotalSeconds;
        }

        //Mètodo que obtiene la fecha de la llamada con el operador.
        private void obtenerFechaHoraInicioLlamada() 
        { fechaHoraInicioLlamada = obtenerEstadoActual(cambioEstado).getFechaHoraInicio();}

        //Método que...
        public void FinalizarLlamada(DateTime fechaHoraActual)
        {
            this.estadoActual.finalizarLlamada(fechaHoraActual, this, cambioEstado);
        }
    }

    #endregion
}
    

