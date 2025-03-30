using AutoMapper;
using IOT.DataStore;
using IOT.Models;
using IOT.Models.DTOs;
using IOT.Repository.IRepository;
using IOT.Services;
using Microsoft.EntityFrameworkCore;

namespace IOT.Repository
{
    public class ClientRecordRepository : IClientRecordRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IClientRepository clientRepository;
        private readonly IMapper mapper;
        private readonly IMailService mailService;

        public ClientRecordRepository(ApplicationDbContext context, IClientRepository clientRepository, IMapper mapper, IMailService mailService)
        {
            this.context = context;
            this.clientRepository = clientRepository;
            this.mapper = mapper;
            this.mailService = mailService;
        }
        public async Task DeleteRecord(int recordID)
        {
            var record = await context.ClientRecords.FirstOrDefaultAsync(x => x.Id == recordID);
            if (record != null)
            {
                context.ClientRecords.Remove(record);
                await Save();

            }
        }

        public async Task enter(int clientId)
        {
            var client = await clientRepository.GetClient(clientId);
            client.status = "in";
            var record = new ClientRecord() { RFID = clientId, EntranaceTime = DateTime.Now };
            await context.ClientRecords.AddAsync(record);
            await Save();
        }

        public async Task exit(int clientId)
        {
            var pricePerHour = 50;
            var record = await context.ClientRecords.Where(x => x.RFID == clientId).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            record.ExitTime = DateTime.Now;
            var timespent = (int)(record.ExitTime - record.EntranaceTime).TotalMinutes;
            //decimal sessionPrice = (timespent / 60) * pricePerHour;
            //decimal sessionPrice = timespent * pricePerHour;
            decimal sessionPrice = 20;
            record.Price = Math.Round(sessionPrice, 2);
            await Save();
            var client = await clientRepository.GetClient(clientId);
            decimal newTotalPrice = client.TotalPrice + sessionPrice;
            await clientRepository.UpdateClient(clientId, new UpdateClientDTO() { TotalPrice = newTotalPrice, status = "out" });
            await Save();
            var target = client.Email;
            var message = $@"
    <html>
    <head>
        <style>
            body {{ font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 20px; }}
            .container {{ background: #ffffff; padding: 20px; border-radius: 10px; box-shadow: 0 2px 5px rgba(0,0,0,0.1); }}
            h2 {{ color: #333; }}
            p {{ font-size: 16px; color: #555; }}
            .highlight {{ font-weight: bold; color: #007bff; }}
            .footer {{ margin-top: 20px; font-size: 12px; color: #888; }}
        </style>
    </head>
    <body>
        <div class='container'>
            <h2>Hello {client.Name},</h2>
            <p>We would like to inform you that your RFID Card has been used during the period:</p>
            <p>
                <span class='highlight'>{record.EntranaceTime:yyyy-MM-dd HH:mm}</span> to
                <span class='highlight'>{record.ExitTime:yyyy-MM-dd HH:mm}</span>
            </p>
            <p>You have been charged <span class='highlight'>{record.Price}$</span>.</p>
            <p>Your total charges are now: <span class='highlight'>{client.TotalPrice}$</span>.</p>
            <p>Thank you for using our service.</p>
            <div class='footer'>This is an automated message. Please do not reply.</div>
        </div>
    </body>
    </html>";

            var IsScuccess = await sendMail(target, message);
            if (!IsScuccess) throw new Exception("error in sending mail");
        }


        public async Task ManipulateRecord(int clientId)
        {
            var client = await clientRepository.GetClient(clientId);
            if (client.status == "out")
            {
                await enter(clientId);
            }
            else
            {
                await exit(clientId);
            }
        }
        public async Task<bool> sendMail(string target, string message)
        {

            var subject = "University Gate Service";

            bool Success = await mailService.SendEmailAsync(target, subject, message);
            return Success;

        }



        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
