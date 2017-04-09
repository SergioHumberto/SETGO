﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WebApplicationTemplate.Objects;
using WebApplicationTemplate.DAL;

namespace WebApplicationTemplate.BLL
{
    public class CarreraBLL
    {
        public void InsertarCarrera(CarreraOBJ carreraOBJ)
        {
			CarreraDAL.InsertarCarrera(carreraOBJ);
        }
    }
}
