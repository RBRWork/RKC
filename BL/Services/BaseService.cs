﻿using BE.Counter;
using DB.DataBase;
using DB.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface IBaseService
    {
        /// <summary>
        /// Информация заркыт или открыт лицевой счет
        /// </summary>
        /// <param name="FullLic"></param>
        /// <returns></returns>
        bool GetStatusCloseOpenLic(string FullLic);
        /// <summary>
        /// заркыт или открыт лицевой счет
        /// </summary>
        /// <param name="FullLic"></param>
        /// <returns></returns>
        Task ClosePersInLic(string FullLic);
    }
    public class BaseService : IBaseService
    {
        public bool GetStatusCloseOpenLic(string FullLic)
        {
            using (var dbLic =new DbLIC())
            {
                var Zak = dbLic.ALL_LICS.FirstOrDefault(x => x.F4ENUMELS == FullLic);
                if(Zak != null && Zak.ZAK != null)
                {
                    return true;
                }
                if (Zak != null && Zak.ZAK == null)
                {
                    return false;
                }
                return false;
            }
        }
        public async Task ClosePersInLic(string FullLic)
        {
            using (var dbfSql = new DbLIC())
            {
                var Lic = await dbfSql.ALL_LICS.FirstOrDefaultAsync(x => x.F4ENUMELS == FullLic);
                Lic.SOBS = 0;
                Lic.KL = 0;
                await dbfSql.SaveChangesAsync();
            }
        }
        public async Task UpdateLicReadings( SaveModelIPU saveModelIPU)
        {
            if (saveModelIPU.FKUB2XVS != null)
            {
                using (var DbLIC = new DbLIC())
                {
                    ALL_LICS aLL_LICS = await DbLIC.ALL_LICS.Where(x => x.F4ENUMELS == saveModelIPU.FULL_LIC).FirstOrDefaultAsync();
                    if (saveModelIPU.OVERWRITE_SEAL)
                    {
                        aLL_LICS.FKUB2XVS = saveModelIPU.FKUB2XVS == null ? aLL_LICS.FKUB2XVS : saveModelIPU.FKUB2XVS;
                        aLL_LICS.FKUB1XVS = saveModelIPU.FKUB1XVS == null ? aLL_LICS.FKUB1XVS : saveModelIPU.FKUB1XVS;
                        aLL_LICS.FKUBSXVS = 1;
                        await DbLIC.SaveChangesAsync();
                    }
                }
            }
            if (saveModelIPU.FKUB2XV_2 != null)
            {
                using (var DbLIC = new DbLIC())
                {
                    ALL_LICS aLL_LICS = await DbLIC.ALL_LICS.Where(x => x.F4ENUMELS == saveModelIPU.FULL_LIC).FirstOrDefaultAsync();
                    if (saveModelIPU.OVERWRITE_SEAL)
                    {
                        aLL_LICS.FKUB2XV_2 = saveModelIPU.FKUB2XV_2 == null ? aLL_LICS.FKUB2XV_2 : saveModelIPU.FKUB2XV_2;
                        aLL_LICS.FKUB1XV_2 = saveModelIPU.FKUB1XV_2 == null ? aLL_LICS.FKUB1XV_2 : saveModelIPU.FKUB1XV_2;
                        aLL_LICS.FKUBSXV_2 = 1;
                        await DbLIC.SaveChangesAsync();
                    }
                }
            }
            if (saveModelIPU.FKUB2XV_3 != null)
            {
                using (var DbLIC = new DbLIC())
                {
                    ALL_LICS aLL_LICS = await DbLIC.ALL_LICS.Where(x => x.F4ENUMELS == saveModelIPU.FULL_LIC).FirstOrDefaultAsync();
                    if (saveModelIPU.OVERWRITE_SEAL)
                    {
                        aLL_LICS.FKUB2XV_3 = saveModelIPU.FKUB2XV_3 == null ? aLL_LICS.FKUB2XV_3 : saveModelIPU.FKUB2XV_3;
                        aLL_LICS.FKUB1XV_3 = saveModelIPU.FKUB1XV_3 == null ? aLL_LICS.FKUB1XV_3 : saveModelIPU.FKUB1XV_3;
                        aLL_LICS.FKUBSXV_3 = 1;
                        await DbLIC.SaveChangesAsync();
                    }
                }
            }
            if (saveModelIPU.FKUB2XV_4 != null)
            {
                using (var DbLIC = new DbLIC())
                {
                    ALL_LICS aLL_LICS = await DbLIC.ALL_LICS.Where(x => x.F4ENUMELS == saveModelIPU.FULL_LIC).FirstOrDefaultAsync();
                    if (saveModelIPU.OVERWRITE_SEAL)
                    {
                        aLL_LICS.FKUB2XV_4 = saveModelIPU.FKUB2XV_4 == null ? aLL_LICS.FKUB2XV_4 : saveModelIPU.FKUB2XV_4;
                        aLL_LICS.FKUB1XV_4 = saveModelIPU.FKUB1XV_4 == null ? aLL_LICS.FKUB1XV_4 : saveModelIPU.FKUB1XV_4;
                        aLL_LICS.FKUBSXV_4 = 1;
                        await DbLIC.SaveChangesAsync();
                    }
                }
            }
            if (saveModelIPU.FKUB2OT_1 != null)
            {
                using (var DbLIC = new DbLIC())
                {

                    ALL_LICS aLL_LICS = await DbLIC.ALL_LICS.Where(x => x.F4ENUMELS == saveModelIPU.FULL_LIC).FirstOrDefaultAsync();
                    if (saveModelIPU.OVERWRITE_SEAL)
                    {
                        aLL_LICS.FKUB2OT_1 = saveModelIPU.FKUB2OT_1 == null ? aLL_LICS.FKUB2OT_1 : saveModelIPU.FKUB2OT_1;
                        aLL_LICS.FKUB1OT_1 = saveModelIPU.FKUB1OT_1 == null ? aLL_LICS.FKUB1OT_1 : saveModelIPU.FKUB1OT_1;
                        aLL_LICS.FKUBSOT_1 = 1;
                        await DbLIC.SaveChangesAsync();
                    }
                }
            }
            if (saveModelIPU.FKUB2OT_2 != null)
            {
                using (var DbLIC = new DbLIC())
                {
                    ALL_LICS aLL_LICS = await DbLIC.ALL_LICS.Where(x => x.F4ENUMELS == saveModelIPU.FULL_LIC).FirstOrDefaultAsync();
                    if (saveModelIPU.OVERWRITE_SEAL)
                    {
                        aLL_LICS.FKUB2OT_2 = saveModelIPU.FKUB2OT_2 == null ? aLL_LICS.FKUB2OT_2 : saveModelIPU.FKUB2OT_2;
                        aLL_LICS.FKUB1OT_2 = saveModelIPU.FKUB1OT_2 == null ? aLL_LICS.FKUB1OT_2 : saveModelIPU.FKUB1OT_2;
                        aLL_LICS.FKUBSOT_2 = 1;
                        await DbLIC.SaveChangesAsync();
                    }
                }
            }
            if (saveModelIPU.FKUB2OT_3 != null)
            {

                using (var DbLIC = new DbLIC())
                {
                    ALL_LICS aLL_LICS = await DbLIC.ALL_LICS.Where(x => x.F4ENUMELS == saveModelIPU.FULL_LIC).FirstOrDefaultAsync();
                    if (saveModelIPU.OVERWRITE_SEAL)
                    {
                        aLL_LICS.FKUB2OT_3 = saveModelIPU.FKUB2OT_3 == null ? aLL_LICS.FKUB2OT_3 : saveModelIPU.FKUB2OT_3;
                        aLL_LICS.FKUB1OT_3 = saveModelIPU.FKUB1OT_3 == null ? aLL_LICS.FKUB1OT_3 : saveModelIPU.FKUB1OT_3;
                        aLL_LICS.FKUBSOT_3 = 1;
                        await DbLIC.SaveChangesAsync();
                    }
                }
            }
            if (saveModelIPU.FKUB2OT_4 != null)
            {
                using (var DbLIC = new DbLIC())
                {
                    ALL_LICS aLL_LICS = await DbLIC.ALL_LICS.Where(x => x.F4ENUMELS == saveModelIPU.FULL_LIC).FirstOrDefaultAsync();
                    if (saveModelIPU.OVERWRITE_SEAL)
                    {
                        aLL_LICS.FKUB2OT_4 = saveModelIPU.FKUB2OT_4 == null ? aLL_LICS.FKUB2OT_4 : saveModelIPU.FKUB2OT_4;
                        aLL_LICS.FKUB1OT_4 = saveModelIPU.FKUB1OT_4 == null ? aLL_LICS.FKUB1OT_4 : saveModelIPU.FKUB1OT_4;
                        aLL_LICS.FKUBSOT_4 = 1;
                        await DbLIC.SaveChangesAsync();
                    }
                }
            }
        }
    }
}
