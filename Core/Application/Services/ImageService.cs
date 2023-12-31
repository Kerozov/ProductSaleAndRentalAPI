﻿using System.Net;
using Application.Helpers.Constants;
using Application.Models.ExceptionModel;
using Application.Models.ImageModels.Dtos;
using Application.Models.ImageModels.Interface;
using Application.Models.ProductModels.Interface;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Application.Services;

public class ImageService : IImageService
{
    private readonly IImageRepository _imageRepository;
    private readonly IConfiguration _config;
    private readonly IProductRepository _productRepository;
    private readonly Cloudinary _cloudinary;


    public ImageService(IImageRepository imageRepository, IConfiguration config, IProductRepository productRepository)
    {
        _imageRepository = imageRepository;
        _config = config;
        _productRepository = productRepository;
        var cloudinaryAccount = new Account(
            _config["Cloudinary:CloudName"],
            _config["Cloudinary:APIKey"],
            _config["Cloudinary:APISecret"]);
        _cloudinary = new Cloudinary(cloudinaryAccount);
    }

    public async Task DeleteImages(int productId)
    {
        var entity = await _productRepository.GetById(productId);

        if (entity == null)
        {
            throw new HttpException($"Product Id not found!", HttpStatusCode.NotFound);
        }
        var image = await _imageRepository.GetImageByProductId(productId);
        
        
        if (image != null)
        {
            _cloudinary.Destroy(new DeletionParams(image.img));
            await _imageRepository.Delete(image.Id);
        }
    }

    public async Task<ImageDto> UploadImage(int productId, AddImageDto image)
    {
        var entity = await _productRepository.GetById(productId);

        if (entity == null)
        {
            throw new HttpException($"Product Id not found!", HttpStatusCode.NotFound);
        }

        _cloudinary.Api.Secure = true;

        //Turn file into byte array
        byte[] bytes;
        using (var memoryStream = new MemoryStream())
        {
            image.Image.CopyTo(memoryStream);
            bytes = memoryStream.ToArray();
        }

        string base64 = Convert.ToBase64String(bytes);

        //construct image path

        var prefix = @"data:image/png;base64,";
        var imagePath = prefix + base64;

        //upload to cloudinary
        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(imagePath),
            Folder = "MarketPlace/"
        };

        var uploadResult = _cloudinary.Upload(@uploadParams);

        await SaveImageInDatabase(productId, uploadResult.PublicId);
        
        var imageDto = new ImageDto()
        {
            URL = CreateURL(uploadResult.PublicId),
        };
        
        return imageDto;
    }

    private async Task SaveImageInDatabase(int productId, string publicId)
    {
        Image newImage = new Image()
        {
            ProductId = productId,
            img = publicId
        };

        await _imageRepository.Create(newImage);
    }

    public async Task<ImageDto> EditImage(int productId, AddImageDto image)
    {
        await DeleteImages(productId);
       return await UploadImage(productId, image);
      
    }
    
    private static string CreateURL(string url)
    {
        return  CloudinaryConstants.baseUrl + url;
    }
}