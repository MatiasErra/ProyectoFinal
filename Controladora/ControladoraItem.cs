using Clases;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controladoras
{
    public class ControladoraItem
    {
        #region Instancia

        private static ControladoraItem _instancia;

        public static ControladoraItem obtenerInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new ControladoraItem();

            }
            return _instancia;

        }

        #endregion

        #region Depositos

  

       
        public List<Deposito> buscarDepositoFiltro(string buscar, string ordenar)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<Deposito> lst = inst.buscarDepositoFiltro(buscar, ordenar);
            return lst;
        }

        public Deposito buscarDeps(int id)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            Deposito deposito = inst.buscarDeps(id);
            return deposito;

        }

        public bool altaDeps(Deposito deposito)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            if (inst.altaDeps(deposito))
            {
                return true;
            }
            else
                return false;
        }

        public bool bajaDeps(int id)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();


            if (inst.bajaDeps(id))
            {
                return true;
            }
            else
                return false;
        }

        public bool modDeps(Deposito deposito)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            if (inst.modDeps(deposito))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Granjas


 
        public List<Granja> buscarGranjaFiltro(string buscar, string orden)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<Granja> lst = inst.buscarGranjaFiltro(buscar, orden);
            return lst;
        }

        public Granja buscarGranja(int id)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            Granja granja = inst.buscarGranja(id);
            return granja;

        }

        public bool altaGranja(Granja granja)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            if (inst.altaGranja(granja))
            {
                return true;
            }
            else
                return false;
        }

        public bool bajaGranja(int id)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();


            if (inst.bajaGranja(id))
            {
                return true;
            }
            else
                return false;
        }

        public bool modGranja(Granja granja)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            if (inst.modGranja(granja))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Productos

        public List<Producto> buscarProductoFiltro(string buscar, string tipo, string tipoVen, string ordenar)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<Producto> lst = inst.buscarProductoFiltro(buscar, tipo, tipoVen, ordenar);
            return lst;
        }


        public Producto buscarProducto(int id)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            Producto producto = inst.buscarProducto(id);
            return producto;

        }

        public bool altaProducto(Producto producto)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            if (inst.altaProducto(producto))
            {
                return true;
            }
            else
                return false;
        }

        public bool bajaProducto(int id)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();


            if (inst.bajaProducto(id))
            {
                return true;
            }
            else
                return false;
        }

        public bool modProducto(Producto producto)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            if (inst.modProducto(producto))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Pesticida

    

      

        public List<Pesticida> buscarPesticidaFiltro(string buscar, string impact, string ordenar)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<Pesticida> lst = inst.buscarPesticidaFiltro(buscar, impact, ordenar);
            return lst;
        }

        public Pesticida buscarPesti(int id)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            Pesticida pesti = inst.buscarPesti(id);
            return pesti;

        }

        public bool altaPesti(Pesticida pesticida)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            if (inst.altaPesti(pesticida))
            {
                return true;
            }
            else
                return false;
        }

        public bool bajaPesti(int id)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();


            if (inst.bajaPesti(id))
            {
                return true;
            }
            else
                return false;
        }

        public bool modPesti(Pesticida pesticida)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            if (inst.modPesti(pesticida))
            {
                return true;
            }
            else
            {
                return false;
            }
        }




        #endregion

        #region Fertilizantes



        public List<Fertilizante> buscarFertilizanteFiltro(string buscar, string impact, string ordenar)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<Fertilizante> lst = inst.buscarFertilizanteFiltro(buscar, impact, ordenar);
            return lst;
        }

        public Fertilizante buscarFerti(int id)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            Fertilizante fertilizante = inst.buscarFerti(id);
            return fertilizante;

        }

        public bool altaFerti(Fertilizante fertilizante)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            if (inst.altaFerti(fertilizante))
            {
                return true;
            }
            else
                return false;
        }

        public bool bajaFerti(int id)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();


            if (inst.bajaFerti(id))
            {
                return true;
            }
            else
                return false;
        }

        public bool modFerti(Fertilizante fertilizante)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            if (inst.modFerti(fertilizante))
            {
                return true;
            }
            else
            {
                return false;
            }
        }




        #endregion

        #region Lotes

     
        public List<string[]> buscarFiltrarLotes(string buscar, string ordenar)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<string[]> lst = inst.buscarFiltrarLotes(buscar, ordenar);
            return lst;
        }

        public string[] buscarLote(string nombreGranja, string nombreProducto, string fchProduccion)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            string[] lote = inst.buscarLote(nombreGranja, nombreProducto, fchProduccion);
            return lote;

        }

        public bool altaLote(Lote lote, string cantTotal)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            if (inst.altaLote(lote, cantTotal))
            {
                return true;
            }
            else
                return false;
        }

        public bool bajaLote(string nombreGranja, string nombreProducto, string fchProduccion, string cantTotal)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();


            if (inst.bajaLote(nombreGranja, nombreProducto, fchProduccion, cantTotal))
            {
                return true;
            }
            else
                return false;
        }

        public bool modLote(Lote lote, string cantTotal)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            if (inst.modLote(lote, cantTotal))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Lotes_Pestis

 
        public List<string[]> PestisEnLote(int idGranja, int idProducto, string fchProduccion)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<string[]> lst = inst.PestisEnLote(idGranja, idProducto, fchProduccion);
            return lst;
        }

        public Lote_Pesti buscarLotePesti(int idPesicida, int idGranja, int idProducto, string fchProduccion)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            Lote_Pesti loteP = inst.buscarLotePesti(idPesicida, idGranja, idProducto, fchProduccion);
            return loteP;

        }

        public bool altaLotePesti(Lote_Pesti loteP)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            if (inst.altaLotePesti(loteP))
            {
                return true;
            }
            else
                return false;
        }

        public bool bajaLotePesti(int idFertilizante, int idGranja, int idProducto, string fchProduccion)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();


            if (inst.bajaLotePesti(idFertilizante, idGranja, idProducto, fchProduccion))
            {
                return true;
            }
            else
                return false;
        }

        public bool modLotePesti(Lote_Pesti loteP)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            if (inst.modLotePesti(loteP))
            {
                return true;
            }
            else
                return false;
        }

        #endregion

        #region Lotes_Fertis

        public List<string[]> FertisEnLote(int idGranja, int idProducto, string fchProduccion)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<string[]> lst = inst.FertisEnLote(idGranja, idProducto, fchProduccion);
            return lst;
        }

        public Lote_Ferti buscarLoteFerti(int idFertilizante, int idGranja, int idProducto, string fchProduccion)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            Lote_Ferti loteF = inst.buscarLoteFerti(idFertilizante, idGranja, idProducto, fchProduccion);
            return loteF;

        }

        public bool altaLoteFerti(Lote_Ferti loteF)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            if (inst.altaLoteFerti(loteF))
            {
                return true;
            }
            else
                return false;
        }

        public bool bajaLoteFerti(int idFertilizante, int idGranja, int idProducto, string fchProduccion)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();


            if (inst.bajaLoteFerti(idFertilizante, idGranja, idProducto, fchProduccion))
            {
                return true;
            }
            else
                return false;
        }

        public bool modLoteFerti(Lote_Ferti loteF)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            if (inst.modLoteFerti(loteF))
            {
                return true;
            }
            else
                return false;
        }

        #endregion

        #region Camiones
     

    
        public List<Camion> buscarFiltroCam(string buscar, string disp, string ordenar)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            List<Camion> lst = inst.buscarFiltroCam(buscar, disp, ordenar);
            return lst;
        }

        public Camion buscarCam(int id)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            Camion pesti = inst.buscarCam(id);
            return pesti;

        }

        public bool altaCam(Camion camion)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();

            if (inst.altaCam(camion))
            {
                return true;
            }
            else
                return false;
        }

        public bool bajaCam(int id)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();


            if (inst.bajaCam(id))
            {
                return true;
            }
            else
                return false;
        }

        public bool modCam(Camion camion)
        {
            ControladoraI inst = ControladoraI.obtenerInstancia();
            if (inst.modCam(camion))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

    }
}
