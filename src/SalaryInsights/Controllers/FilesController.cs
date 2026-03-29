using System;
using System.Threading;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalaryInsights.Application.Contracts;
using SalaryInsights.Domain.Dtos;
using SalaryInsights.Domain.Dtos.FileStorage;
using SalaryInsights.Extensions;
using SalaryInsights.Infrastructure.Exceptions;

namespace SalaryInsights.Controllers
{
    /// <summary>
    /// Files controller
    /// </summary>
    /// <param name="appService"></param>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class FilesController(IFileStorageAppService appService) : ControllerBase
    {
        /// <summary>
        /// Get file hash
        /// </summary>
        /// <param name="file">The file to compute hash for</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("hash")]
        [Consumes("multipart/form-data")]
        [DisableFormValueModelBinding]
        public async Task<ActionResult<FileHashDto>> GetHash(
            IFormFile file,
            CancellationToken cancellationToken)
        {
            ValidateMultipartRequest();

            return await appService.GetHashAsync(Request.Body, cancellationToken);
        }

        /// <summary>
        /// Upload file
        /// </summary>
        /// <param name="file">The file to upload</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        [DisableFormValueModelBinding]
        public async Task<ActionResult<OperationResult<FileAssetDto>>> Upload(
            IFormFile file,
            CancellationToken cancellationToken)
        {
            ValidateMultipartRequest();

            var boundary = Request.GetMultipartBoundary();
            if (string.IsNullOrWhiteSpace(boundary))
            {
                throw new BadRequestException("Missing boundary in multipart form data.");
            }

            return await appService.UploadAsync(boundary, Request.Body, cancellationToken);
        }

        /// <summary>
        /// Get file analysis
        /// </summary>
        /// <param name="id">File asset identifier</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}/analysis")]
        public async Task Analysis(
            Guid id,
            CancellationToken cancellationToken)
        {
            var result = await appService.AnalysisAsync(id, cancellationToken);
            if (result is null)
                throw new NotFoundException("File Asset not found");

            await foreach (var update in result.WithCancellation(cancellationToken))
            {
                if (string.IsNullOrWhiteSpace(update.Text))
                    continue;

                await Response.WriteAsync(update.Text, cancellationToken);
                await Response.Body.FlushAsync(cancellationToken);
            }
        }

        /// <summary>
        /// Validate if the request is a multipart request
        /// </summary>
        /// <exception cref="UnsupportedMediaTypeException"></exception>
        private void ValidateMultipartRequest()
        {
            if (!Request.IsMultipartContentType())
            {
                throw new UnsupportedMediaTypeException("The request does not contain valid multipart form data.");
            }
        }
    }
}