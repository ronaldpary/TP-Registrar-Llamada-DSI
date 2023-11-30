using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPAI2023.Entidades;
using PPAI2023.Controlador;

namespace PPAI2023.RepositorioDatos
{
    public class BaseDatos
    {
        #region Atributos

        GestorRegistrarRespuesta gestor;

        #endregion 



        //Constructor
        public BaseDatos(GestorRegistrarRespuesta gestor) {

            this.gestor = gestor;       
        }

        public void inicializarDatos()
        {

            EstadoLlamada estado1;
            EstadoLlamada estado2;
            EstadoLlamada estado3;

            using (PPAI_DSIEntities db = new PPAI_DSIEntities())
            {
                estado1 = db.Estados.Single(b => b.nombre_estado == "Iniciada").fromDomain();
                estado2 = db.Estados.Single(b => b.nombre_estado == "EnCurso").fromDomain();
                estado3 = db.Estados.Single(b => b.nombre_estado == "Finalizada").fromDomain();
            }

            using (PPAI_DSIEntities db = new PPAI_DSIEntities())
            {
                Llamadas llamada = new Llamadas();

                Random r = new Random();
                int minutes = r.Next(-10, 0);

                llamada.id_cliente = 1;
                llamada.fecha_hora_inicio = DateTime.Now.AddMinutes(-minutes);

                db.Llamadas.Add(llamada);
                db.SaveChanges();

                CambiosEstado cambiosEstado = new CambiosEstado();

                cambiosEstado.fecha_inicio = DateTime.Now;
                cambiosEstado.id_estado = 9;

                Llamadas llamadaCreada = db.Llamadas
                                    .OrderByDescending(l => l.id_llamada)
                                    .FirstOrDefault();
    
                cambiosEstado.id_llamada = llamadaCreada.id_llamada;

                db.CambiosEstado.Add(cambiosEstado);
                db.SaveChanges();
            }

            List<CategoriaLlamada> categorias;

            using (PPAI_DSIEntities db = new PPAI_DSIEntities())
            {
                categorias = db.Categorias.ToList().Select(a => a.fromDomain()).ToList();
            }
   

            List<OpcionLlamada> opcionesLLamadas1;
            List<OpcionLlamada> opcionesLLamadas2;
            using (PPAI_DSIEntities db = new PPAI_DSIEntities())
            {
                List<Categorias> categoriasBD = db.Categorias.ToList();

                opcionesLLamadas1 = db.OpcionesLlamada
                    .ToList()
                    .FindAll(op => op.id_categoria == categoriasBD[1].id_categoria)
                    .Select(op => op.fromDomain()).ToList();

                opcionesLLamadas2 = db.OpcionesLlamada
                    .ToList()
                    .FindAll(op => op.id_categoria == categoriasBD[2].id_categoria)
                    .Select(op => op.fromDomain()).ToList();
            }

           

            List<SubOpcionLlamada> subOpcionesLlamadas;
            using (PPAI_DSIEntities db = new PPAI_DSIEntities())
            {
                subOpcionesLlamadas = db.SubOpcionesLlamada
                    .ToList()
                    .Select(subop => subop.fromDomain()).ToList();
            }


            List<Validacion> validaciones;
            using (PPAI_DSIEntities db = new PPAI_DSIEntities())
            {
                validaciones = db.Validaciones.ToList().Select(va => va.fromDomain()).ToList();
            }


            List<OpcionValidacion> opcionValidaciones;

            using (PPAI_DSIEntities db = new PPAI_DSIEntities())
            {
                opcionValidaciones = db.OpcionesValidaciones
                    .ToList()
                    .Select(opV => opV.fromDomain())
                    .ToList();
            }


            //Cliente
            Cliente clienteLlamada;

            //Acciones
            List<Accion> acciones;
            using (PPAI_DSIEntities db = new PPAI_DSIEntities())
            {
                acciones = db.Acciones.ToList().Select(a => a.fromDomain()).ToList();
                clienteLlamada = db.Clientes
                    .Single(c => c.nombre == "Nicólas Ranalli")
                    .fromDomain();
            }

            //Objetos traidos del CU1 y carga de datos

            //FechaHoraInicioLlamada 
            //Le resto 5 minutos que son en los que esta con el BOT
            DateTime fechaHoraInicio = DateTime.Now.AddMinutes(-5);

         
            //Llamada del cliente
            Llamada llamadaActual;
            
            using (PPAI_DSIEntities db = new PPAI_DSIEntities())
            {
                CambiosEstado cambioActual = db.CambiosEstado.Single(cbE => cbE.fecha_fin == null);

                llamadaActual = db.Llamadas
                    .Single(l => l.id_llamada == cambioActual.id_llamada).fromDomain();
                    }



            categorias[0].agregarOpcion(opcionesLLamadas2[0]);
            categorias[0].agregarOpcion(opcionesLLamadas2[1]);
            categorias[0].agregarOpcion(opcionesLLamadas2[2]);
            categorias[0].agregarOpcion(opcionesLLamadas2[3]);

            categorias[1].agregarOpcion(opcionesLLamadas1[0]);
            categorias[1].agregarOpcion(opcionesLLamadas1[1]);
            categorias[1].agregarOpcion(opcionesLLamadas1[2]);

            opcionesLLamadas2[0].agregarSubOpcion(subOpcionesLlamadas[0]);
            opcionesLLamadas2[0].agregarSubOpcion(subOpcionesLlamadas[1]);
            opcionesLLamadas2[0].agregarSubOpcion(subOpcionesLlamadas[2]);
            opcionesLLamadas2[0].agregarSubOpcion(subOpcionesLlamadas[3]);

            validaciones[0].agregarOpcionValidacion(opcionValidaciones[0]);
            validaciones[0].agregarOpcionValidacion(opcionValidaciones[1]);
            validaciones[0].agregarOpcionValidacion(opcionValidaciones[2]);

            validaciones[1].agregarOpcionValidacion(opcionValidaciones[3]);
            validaciones[1].agregarOpcionValidacion(opcionValidaciones[4]);
            validaciones[1].agregarOpcionValidacion(opcionValidaciones[5]);

            validaciones[2].agregarOpcionValidacion(opcionValidaciones[6]);
            validaciones[2].agregarOpcionValidacion(opcionValidaciones[7]);
            validaciones[2].agregarOpcionValidacion(opcionValidaciones[8]);

            subOpcionesLlamadas[3].agregarValidacion(validaciones[0]);
            subOpcionesLlamadas[3].agregarValidacion(validaciones[1]);
            subOpcionesLlamadas[3].agregarValidacion(validaciones[2]);


            //carga de datos al gestor

            gestor.agregarAcciones(acciones[0]);
            gestor.agregarAcciones(acciones[1]);
            gestor.agregarAcciones(acciones[2]);

            gestor.agregarEstados(estado1);
            gestor.agregarEstados(estado2);
            gestor.agregarEstados(estado3);

            gestor.agregarCategoria(categorias[0]);
            gestor.agregarOpcion(opcionesLLamadas1[0]);
            gestor.agregarSubOpcion(subOpcionesLlamadas[3]);

            gestor.opcionLlamadaOperador(llamadaActual);

        }


    }
}
